using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OmniPot.Data;
using OmniPot.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace OmniPot.Controllers.Admin
{
    [Route("api/[controller]")]
    [Authorize(Roles = "SuperAdmin,TechSupport,TenantAdmin")] 
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext appContext;
        private readonly KindDbContext kindContext; 
        public UsersController(ApplicationDbContext appContext, KindDbContext kindContext)
        {
            this.appContext = appContext;
            this.kindContext = kindContext; 
        }

        [HttpGet("GetList")]

        public DataSourceResult GetList([DataSourceRequest]DataSourceRequest request)
        {
            var result = appContext.Users
                .ToDataSourceResult(request, u => new { Id = u.Id, UserName = u.UserName, Email = u.Email, PhoneNumber = u.PhoneNumber });

            return result;
        }

        [HttpGet]
        public IEnumerable<ApplicationUser> Get()
        {
            return this.appContext.Users.OrderBy(u => u.UserName); 
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ApplicationUser Get(string id)
        {
            return this.appContext.Users.Single(u => u.Id == id); 
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]ApplicationUser value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
        }
    }
}
