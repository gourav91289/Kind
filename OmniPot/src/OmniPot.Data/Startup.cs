using OmniPot.Common;
using OmniPot.Data.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OmniPot.Data
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath);          

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer().AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
             options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OmniPot;Trusted_Connection=True;MultipleActiveResultSets=true")
            .UseInternalServiceProvider(serviceProvider));       

            //services.AddEntityFrameworkSqlServer().AddDbContext<KindDbContext>((serviceProvider, options) =>
            //options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OmniPot;Trusted_Connection=True;MultipleActiveResultSets=true")
            //.UseInternalServiceProvider(serviceProvider));

            services.AddTransient<IUserContext, SeedUserContext>();
        }
        public void Configure(IApplicationBuilder app)
        {

        }
    }
}
