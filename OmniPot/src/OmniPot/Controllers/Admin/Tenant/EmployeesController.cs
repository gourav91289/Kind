using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OmniPot.Data;
using OmniPot.Data.Identity;
using OmniPot.Data.Models;
using OmniPot.Services;
using OmniPot.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Controllers
{
    [Produces("application/json")]
    [Route("api/{tenant}/Employees")]
    [Authorize(Roles ="SuperAdmin,TenantAdmin,TechSupport")]
    public class EmployeesController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager; 

        public EmployeesController(KindDbContext context, TenantCacheService tenantCacheService, ILoggerFactory loggerFactory, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
            : base(context, tenantCacheService, loggerFactory)
        {
            this.userManager = userManager;
            this.roleManager = roleManager; 
        }
        
        [HttpGet(Name = "GetEmployees")]        
        public IEnumerable<Employee> GetEmployees([FromRoute]string tenant)
        {
            //TODO: This list should only project the members needed for a grid. The Singular get should get the deep copy of the employee entity for editing.
            var tenantId = tenantCacheService.GetId(tenant);
            return context.Tenants
                .Include(t => t.Employees)
                .ThenInclude(e=> e.Address)
                .Include(e => e.Employees)
                .ThenInclude(e => e.Vehicle)
                .Include(t => t.Employees)
                .ThenInclude(e => e.Documents)
                .Where(e => e.TenantId == tenantId)
                .Single()
                .Employees;
        }

        // GET: api/Employees/5
        [HttpGet("{id}", Name = "GetEmployee")]
        public async Task<IActionResult> GetEmployee([FromRoute]string tenant, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Restricting this just for the off chance so that gets are also locked to the tenant in the uri
            var tenantId = tenantCacheService.GetId(tenant);
            Employee employee = await context.Employees.Include(e => e.Address).Include(e => e.Vehicle).SingleAsync(m => m.EmployeeId == id && m.TenantId == tenantId);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        //TODO: Test that it's approprate for the string to come from the body, it probably needs a valid json object and i'm not certain of how the serializer will behave here.
        [HttpPost(Name = "GetUserId")]
        [Route("GetUserId/{email}")]
        public async Task<IActionResult> GetUserId([FromRoute] string tenant, [FromRoute] string email)
        {            
            //TODO: Is the tenant strictly required here? There is no way to determine from the identity framework which tenant a user is with, just from the kind context.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByEmailAsync(email);
                     
            if (null == user)
            {
                return NotFound();
            }
            return Ok(new { id = user.Id });
        }

        [HttpPost(Name = "CreateEmployeeUser")]
        [Route("CreateEmployeeUser")]
        public async Task<IActionResult> CreateEmployeeUser([FromRoute]string tenant, [FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByEmailAsync(model.Email);
            if (null == user)
            {
                user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                await userManager.CreateAsync(user, model.Password);
                await userManager.SetPhoneNumberAsync(user, model.Phone);
                await userManager.SetTwoFactorEnabledAsync(user, true);
                //TODO: determine flags for lockout and what not. It could be that we want to create the account but keep it locked until another process (like update employee) updates it.
            }
            return Ok(new { id = user.Id });
        }

        [HttpPost("AddEmployeeToRoles/{id}")]
        public async Task<IActionResult> AddEmployeeToRoles([FromRoute]string tenant, [FromRoute]Guid id, [FromBody]string[] roles)
        {
            var tenantId = tenantCacheService.GetId(tenant);
            var employee = await context.Employees.Where(e => e.TenantId == tenantId && e.EmployeeId == id && e.State == TrackableEntityState.IsActive).SingleOrDefaultAsync();
            if (null != employee)
            {
                var user = await userManager.FindByIdAsync(employee.UserId);
                if (null != user)
                { 
                    await userManager.AddToRolesAsync(user, roles);
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest();
        }

        [HttpPost("LockEmployeeUser/{emailAddress}", Name = "LockEmployeeUser")]
        public async Task<IActionResult> LockEmployeeUser([FromRoute]string tenant, [FromRoute]string emailAddress)
        {
            //TODO: AuthorizeAttribute, ensure this user can indeed lock an account.             
            var user = await userManager.FindByEmailAsync(emailAddress); 
            if (null != user)
            {
                var tenantId = tenantCacheService.GetId(tenant);
                if (context.Employees.Any(e => e.TenantId == tenantId && e.UserId == user.Id))
                {
                    await userManager.SetLockoutEnabledAsync(user, true);
                    return Ok(emailAddress);
                }
                return NotFound(emailAddress);
            }
            return NotFound(emailAddress);
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee([FromRoute]string tenant, [FromRoute] Guid id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            //There isn't anything to set this on the client, but we shouldn't be allowing them to set the tenant id to something other than what's in 
            //the route. 
            employee.TenantId = tenantCacheService.GetId(tenant);
            context.Entry(employee).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
                ApplyClaims(employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromRoute]string tenant, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            employee.TenantId = tenantCacheService.GetId(tenant);            
            context.Employees.Add(employee);
            try
            {
                await context.SaveChangesAsync();
                ApplyClaims(employee);
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(employee.EmployeeId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            //TODO: Determine if this needs modified with the tenant
            return CreatedAtRoute("GetEmployee", new { id = employee.EmployeeId }, employee);
        }

        private async void ApplyClaims(Employee employee)
        {
            //HACK: This check should actually be removed once this is implemented. Going from having allowed locations to no allowed locations will need to be handled below in the usermanager call to remove.
            if (employee.AllowedLocations.Count < 1)
                return;

            var user = await userManager.FindByIdAsync(employee.UserId);
            if (null != user)
            {
                //Do we blast all claims here? 
                var claims = await userManager.GetClaimsAsync(user);
                var locationClaims = claims.Where(e => e.Type == "location");
                await userManager.RemoveClaimsAsync(user, locationClaims);
                foreach (var location in employee.AllowedLocations)
                {
                    await userManager.AddClaimAsync(user, new System.Security.Claims.Claim("location", location.RouteName));                    
                }
            }
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] string tenant, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tenantId = tenantCacheService.GetId(tenant);
            Employee employee = await context.Employees.SingleAsync(m => m.EmployeeId == id && m.TenantId == tenantId);
            if (employee == null)
            {
                return NotFound();
            }

            employee.State = TrackableEntityState.IsDeleted;
            context.Entry(employee).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return Ok(employee);
        }



        private bool EmployeeExists(Guid id)
        {
            return context.Employees.Count(e => e.EmployeeId == id) > 0;
        }
    }
}