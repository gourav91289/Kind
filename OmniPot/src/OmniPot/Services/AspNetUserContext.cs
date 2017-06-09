using OmniPot.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OmniPot.Data.Identity;

namespace OmniPot.Services
{
    public class AspNetUserContext : IUserContext
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly UserManager<ApplicationUser> userManager;

        public AspNetUserContext(IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager)
        {
            this.contextAccessor = contextAccessor;
            this.userManager = userManager;
        }
        public ClaimsPrincipal CurrentUser
        {
            get
            {
                //HACK: Why is contextAccessor.HttpContext ever null? 
                //return contextAccessor.HttpContext.User;
                return null == contextAccessor.HttpContext ? null : contextAccessor.HttpContext.User;
            }
        }

        public Guid UserId
        {
            get
            {
                return null == CurrentUser ? Guid.Empty : null == userManager.GetUserId(CurrentUser) ? Guid.Empty : Guid.Parse(userManager.GetUserId(CurrentUser));
            }
        }
    }
}
