using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using OmniPot.Data.Identity;
using OmniPot.Common;

namespace OmniPot.Data
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
    //options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OmniPot;Trusted_Connection=True;MultipleActiveResultSets=true")
    options.UseSqlServer("Server=NTZ-MANOJSINGH\\MSSQL;Database=OmniPot;User Id=sa;Password=P@55w0rd;MultipleActiveResultSets=true")
    
                         .UseInternalServiceProvider(serviceProvider));
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<KindDbContext>((serviceProvider, options) =>
       // options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OmniPot;Trusted_Connection=True;MultipleActiveResultSets=true")
       options.UseSqlServer("Server=NTZ-MANOJSINGH\\MSSQL;Database=OmniPot;User Id=sa;Password=P@55w0rd;MultipleActiveResultSets=true")
           .UseInternalServiceProvider(serviceProvider));
            services.AddTransient<IUserContext, SeedUserContext>();
        }
        public void Configure(IApplicationBuilder app)
        {

        }
    }
}
