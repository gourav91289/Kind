using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OmniPot.Data;
using System.Collections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace OmniPot.Controllers
{
    [Produces("application/json")]
    [Route("api/List")]
    //[Authorize]
    public class ListController : Controller
    {
        private readonly KindDbContext context;
        private readonly RoleManager<IdentityRole> roleManager; 

        public ListController(KindDbContext context, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.roleManager = roleManager; 
        }

        // GET: api/List
        [HttpGet()]
        [Route("Countries")]
        public IEnumerable GetCountries() 
        {
            return context.Countries
                .Where(c => c.State == TrackableEntityState.IsActive)
                .OrderBy(c => c.DisplayName)
                .Select(c => new { Id = c.CountryId, DisplayName = c.DisplayName }); 
        }

        [HttpGet()]
        [Route("States")]
        public IEnumerable GetStates()
        {
            return context.StatesOrProvinces
                .Where(c => c.State == TrackableEntityState.IsActive)
                .OrderBy(c => c.DisplayName)
                .Select(c => new { Id = c.StateOrProvinceId, DisplayName = c.DisplayName }); 
        }

        [HttpGet]
        [Route("UOM")]
        public IEnumerable GetUOM()
        {
            return context.UnitsOfMeasure
                .Where(c => c.State == TrackableEntityState.IsActive)
                .OrderBy(c => c.DisplayName)
                .Select(c => new { Id = c.UnitOfMeasureId, DisplayName = c.DisplayName });
        }

        [HttpGet()]
        [Route("TenantRoutes")]
        public IEnumerable GetTenantRoutes()
        {
            return context.Tenants.Where(t => t.State == TrackableEntityState.IsActive)
                .OrderBy(t => t.DisplayName)
                .Select(t => new { t.RouteName, t.DisplayName }); 
        }

        [HttpGet()]
        [Route("{tenant}/Locations")]
        public IEnumerable GetTenantLocations([FromRoute]string tenant)
        {
            
            return context.Locations
                .Where(l => l.Tenant.RouteName == tenant && l.State == TrackableEntityState.IsActive)
                .OrderBy(l => l.DisplayName)
                .Select(l => new { Id = l.LocationId, DisplayName = l.DisplayName });
        }

        [HttpGet()]
        [Route("{tenant}/LocationRoutes")]
        public IEnumerable GetTenantLocationRoutes([FromRoute]string tenant)
        {

            return context.Locations
                .Where(l => l.Tenant.RouteName == tenant && l.State == TrackableEntityState.IsActive)
                .OrderBy(l => l.DisplayName)
                .Select(l => new { l.RouteName, DisplayName = l.DisplayName });
        }

        [HttpGet("Roles")]
        public IEnumerable GetRoles()
        {
            return roleManager.Roles.OrderBy(r => r.Name).Select(r => new { r.Id, r.Name });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }       
    }
}