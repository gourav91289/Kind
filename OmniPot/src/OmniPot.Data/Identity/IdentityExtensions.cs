using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace OmniPot.Data.Identity
{
    public static class IdentityExtensions
    {
        public static ApplicationDbContext getApplicationDbContext(this IApplicationBuilder app) {
            return app.ApplicationServices.GetService<ApplicationDbContext>();
        }


        public static void EnsureRolesCreated(this IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService<ApplicationDbContext>();
            if (context.AllMigrationsApplied())
            {
                var roleManager = app.ApplicationServices.GetService<RoleManager<IdentityRole>>();
                foreach (var role in Roles.All)
                {
                    if (!roleManager.RoleExistsAsync(role.ToUpper()).Result)
                    {
                        roleManager.CreateAsync(new IdentityRole { Name = role });
                    }
                }
            }
        }


        public static void ApplyRolesToDevUsers(this IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService<ApplicationDbContext>(); 
            if (context.AllMigrationsApplied())
            {
                var userManager = app.ApplicationServices.GetService<UserManager<ApplicationUser>>();
                ApplicationUser user;
                try
                {
                    user = userManager.FindByEmailAsync("blaird@gmail.com").Result;
                    userManager.AddToRoleAsync(user, "SuperAdmin");
                }
                catch { }
                try
                {
                    user = userManager.FindByEmailAsync("paul@kind.financial").Result;
                    userManager.AddToRoleAsync(user, "SuperAdmin");                    
                }
                catch { }
                try
                {
                    user = userManager.FindByEmailAsync("scott@kind.financial").Result;
                    userManager.AddToRoleAsync(user, "SuperAdmin");
                }
                catch { }
            }
        }
    }
}
