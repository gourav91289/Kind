using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OmniPot.Data;
using OmniPot.Services;

namespace OmniPot.Controllers
{
    public class SuperAdminController : BaseController
    {

        public SuperAdminController(KindDbContext context, TenantCacheService tenantCacheService, ILoggerFactory loggerFactory)
        : base(context, tenantCacheService, loggerFactory) {
            logMessage("SuperAdminController(constructor)", "SuperAdminController constructor ran.");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Tenants()
        {
            logMessage("SuperAdminController(constructor)", "SuperAdminController.Tenants about to return some view  (Tenants.cshtml?)");
            return View();
        }

        public IActionResult Tenants2() 
        {
            logMessage("SuperAdminController(constructor)", "SuperAdminController.Tenants2 about to return some view  (Tenants2.cshtml?)");
            return View();
        }

        public IActionResult Countries()
        {
            logMessage("SuperAdminController(constructor)", "SuperAdminController.Countries about to return some view  (Countries.cshtml?)");
            return View();
        }

        public IActionResult States()
        {
            logMessage("SuperAdminController(constructor)", "SuperAdminController.States about to return some view  (States.cshtml?)");
            return View();
        }

        public IActionResult Users()
        {
            logMessage("SuperAdminController(constructor)", "SuperAdminController.Users about to return some view  (Users.cshtml?)");
            return View();
        }

        public IActionResult HCgridTest() {
            logMessage("SuperAdminController(constructor)", "SuperAdminController.HCgridTest about to return some view  (HCgridTest.cshtml?)");
            return View();
        }
    }
}