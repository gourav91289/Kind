using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OmniPot.Data;
using OmniPot.Services;
using System;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OmniPot.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(KindDbContext context, TenantCacheService tenantCacheService, ILoggerFactory loggerFactory)
            : base(context, tenantCacheService, loggerFactory) 
        {
            logMessage("HomeController(constructor)", "HomeController constructor ran.");
        }

        // GET: /<controller>/
        [Authorize]
        public IActionResult Index()
        {
            logMessage("HomeController.Index()", "HomeController.Index() ran.");
            return View();
        }
        [Authorize]

        public IActionResult Lists()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
