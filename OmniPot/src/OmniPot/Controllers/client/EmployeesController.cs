using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace OmniPot.Controllers.client
{
    [Route("client/{tenant}/Employees")]
    [Authorize(Roles = "SuperAdmin,TenantAdmin,TechSupport")]
    public class EmployeesController : Controller
    {
        public IActionResult Index([FromRoute]string tenant)
        {
            
            //tenant id lookup and validation etc.
            return View("~/Views/Client/Employees/index.cshtml");
        }
    }
}