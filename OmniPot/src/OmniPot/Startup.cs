using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OmniPot.Common;
using OmniPot.Data;
using OmniPot.Data.Identity;
using OmniPot.Infrastructure.Mappings;
using OmniPot.Services;
using Serilog;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            //No way to log activities in Startup constructor ??
            System.Diagnostics.Debug.WriteLine("Startup ( constructor ) started " + DateTime.Now);

            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            var logFilePath = Path.Combine(env.WebRootPath + "\\logs\\", "OmniPot-{Date}.txt");
            Log.Logger = new LoggerConfiguration()
                //.WriteTo.RollingFile(logFilePath)
                .WriteTo.MongoDB("mongodb://omnipotlogging:DrPzUtuyOp3AWCuWfztmMCUbrAv6v0VtEcwcYIRnynWdMcFhHtzDtFY9JJMkIiksFI32H3HfsrnBNKuADoUqsw==@omnipotlogging.documents.azure.com:10250/logs_prod?ssl=true")
                .WriteTo.Seq("http://localhost:5341")
                //.MinimumLevel.Verbose()
                .CreateLogger();

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //No way to log activities in ConfigureServices ??
            System.Diagnostics.Debug.WriteLine("ConfigureServices started " + DateTime.Now);

            // Declare cors policy 
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", corsBuilder.Build());
            });

            services.AddMemoryCache();
            services.AddSession();
            // Add framework services.
            services.AddTransient<IUserContext, AspNetUserContext>();

            services.AddIdentity<ApplicationUser, IdentityRole>(config => {
                config.User.RequireUniqueEmail = true;
                config.SignIn.RequireConfirmedEmail = true;
                config.Password.RequiredLength = 8;
                //config.Cookies.ApplicationCookie.LoginPath = "/Account/Login";
                config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = async ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 401;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        await Task.Yield();
                    }
                };
            })
                .AddEntityFrameworkStores<Data.Identity.ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services
                .AddDbContext<KindDbContext>(options => options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]))
                .AddDbContext<Data.Identity.ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();

#if (DEBUG)
                options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
#endif
            })
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("es-ES")
                };

                // State what the default culture for your application is. This will be used if no specific culture
                // can be determined for a given request.
                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");

                // You must explicitly state which cultures your application supports.
                // These are the cultures the app supports for formatting numbers, dates, etc.
                options.SupportedCultures = supportedCultures;

                // These are the cultures the app supports for UI strings, i.e. we have localized resources for.
                options.SupportedUICultures = supportedCultures;

                // You can change which providers are configured to determine the culture for requests, or even add a custom
                // provider with your own logic. The providers will be asked in order to provide a culture for each request,
                // and the first to provide a non-null result that is in the configured supported cultures list will be used.
                // By default, the following built-in providers are configured:
                // - QueryStringRequestCultureProvider, sets culture via "culture" and "ui-culture" query string values, useful for testing
                // - CookieRequestCultureProvider, sets culture via "ASPNET_CULTURE" cookie
                // - AcceptLanguageHeaderRequestCultureProvider, sets culture via the "Accept-Language" request header
                //options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
                //{
                //  // My custom request culture logic
                //  return new ProviderCultureResult("en");
                //}));
            });


            services.AddKendo();
            //NOTE: removing this until we determine what we'll replace it with
            //services.AddSignalR(options => { });

            //HACK: Commenting for now, it'll blow up later. 
            //services.Configure<AuthyOptions>(Configuration);

            // Add application services.            
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddTransient<IAuthyService, AuthyService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<TenantCacheService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Microsoft.Extensions.Logging.ILogger logger = loggerFactory.CreateLogger<Startup>();

            // Allow origin cors
            app.UseCors("AllowAll");

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddSerilog();
            loggerFactory.AddDebug();


            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
            try
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<Data.Identity.ApplicationDbContext>().Database.Migrate();
                    serviceScope.ServiceProvider.GetService<Data.KindDbContext>().Database.Migrate();

                    //app.EnsureRolesCreated();//Roles need to be defined before seed data which defines Users and EMployees

                    var userManager = app.ApplicationServices.GetService<UserManager<ApplicationUser>>();
                    var roleManager = app.ApplicationServices.GetService<RoleManager<IdentityRole>>();

                    var context = serviceScope.ServiceProvider.GetService<Data.Identity.ApplicationDbContext>();

                    if (context.AllMigrationsApplied())
                    {
                        if (!context.Roles.Any())
                        {
                            var role = roleManager.CreateAsync(new IdentityRole("SuperAdmin")).Result;
                            //TODO: Seed additional roles here.                
                        }                     

                        if (!context.Users.Any(e => e.UserName == "blaird@gmail.com"))
                        {
                            //HACK: Should never ever call async methods within static voids.
                            var user = new ApplicationUser { UserName = "blaird@gmail.com", Email = "blaird@gmail.com", AuthyUserId = "21459770" };
                            var foo = userManager.CreateAsync(user, "Apple123!").Result;
                            var bar = userManager.SetPhoneNumberAsync(user, "7856408466").Result;
                            var baz = userManager.SetTwoFactorEnabledAsync(user, true).Result;
                            var baaz = userManager.SetLockoutEnabledAsync(user, false).Result;
                            var baaaz = userManager.AddToRoleAsync(user, "SuperAdmin").Result;
                        }

                        if (!context.Users.Any(e => e.UserName == "paul@kind.financial"))
                        {
                            //HACK: Should never ever call async methods within static voids.
                            var user = new ApplicationUser { UserName = "paul@kind.financial", Email = "paul@kind.financial", AuthyUserId = "21459771" };
                            var foo = userManager.CreateAsync(user, "Shadow01!").Result;
                            var bar = userManager.SetPhoneNumberAsync(user, "8169299360").Result;
                            var baz = userManager.SetTwoFactorEnabledAsync(user, true).Result;
                            var baaz = userManager.SetLockoutEnabledAsync(user, false).Result;
                            var baaaz = userManager.AddToRoleAsync(user, "SuperAdmin").Result;
                        }

                        if (!context.Users.Any(e => e.UserName == "scott@kind.financial"))
                        {
                            //HACK: Should never ever call async methods within static voids.
                            var user = new ApplicationUser { UserName = "scott@kind.financial", Email = "scott@kind.financial", AuthyUserId = "21459772" };
                            var foo = userManager.CreateAsync(user, "EasyPass$123").Result;
                            var bar = userManager.SetPhoneNumberAsync(user, "3033303325").Result;
                            var baz = userManager.SetTwoFactorEnabledAsync(user, true).Result;
                            var baaz = userManager.SetLockoutEnabledAsync(user, false).Result;
                            var baaaz = userManager.AddToRoleAsync(user, "SuperAdmin").Result;
                        }
                    }

                    serviceScope.ServiceProvider.GetService<Data.KindDbContext>().EnsureSeedData();
                }
            }
            catch (Exception e)
            {
                if (env.IsDevelopment())
                {
                    //uncomment below while moving to the staging or production
                   // throw;
                }
                /// I just want to eat this for now ==>throw;
            }



            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.ApplyRolesToDevUsers();

                //uncomment below while moving to the staging or production
                #region comment
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<Data.Identity.ApplicationDbContext>().Database.Migrate();
                    serviceScope.ServiceProvider.GetService<Data.KindDbContext>().Database.Migrate();

                    var userManager = app.ApplicationServices.GetService<UserManager<ApplicationUser>>();

                    var context = serviceScope.ServiceProvider.GetService<Data.Identity.ApplicationDbContext>();
                    if (!context.Users.Any(e => e.UserName == "blaird@gmail.com"))
                    {
                        //HACK: Should never ever call async methods within static voids.
                        var user = new ApplicationUser { UserName = "blaird@gmail.com", Email = "blaird@gmail.com", AuthyUserId = "21459770" };
                        var foo = userManager.CreateAsync(user, "Apple123!").Result;
                        var bar = userManager.SetPhoneNumberAsync(user, "7856408466").Result;
                        var baz = userManager.SetTwoFactorEnabledAsync(user, true).Result;
                        var baaz = userManager.SetLockoutEnabledAsync(user, false).Result;
                        var baaaz = userManager.AddToRoleAsync(user, "SuperAdmin").Result;
                    }
                }
                #endregion

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == 404
                    && !Path.HasExtension(context.Request.Path.Value))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();

            AutoMapperConfiguration.Configure();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = false,
                LoginPath = "/Account/Login",
                LogoutPath = "/Account/Logout"
            });

            app.UseKendo(env);

            //TODO: Look into this. This is the authentication middleware required by photoalbum but it 
            //      is probably only necessary due to some other configuration. 
            //app.Use(async (context, next) =>
            //{
            //    if (!context.User.Identities.Any(identity => identity.IsAuthenticated))
            //    {
            //        Claim _claim = new Claim(ClaimTypes.Role, "Admin", ClaimValueTypes.String, "chsakell");
            //        await context.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            //            new ClaimsPrincipal(new ClaimsIdentity(new[] { _claim }, CookieAuthenticationDefaults.AuthenticationScheme)));
            //    }
            //});

            app.UseIdentity();

            //Roles need to be defined before seed data which defines Users and EMployees //app.EnsureRolesCreated();
            app.UseSession();
            // app.UseSignalR<AppConnection>("/Connections/AppConnection");
            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            //app.Use(async (context, next) =>
            //{
            //    if (context.User.Identity.IsAuthenticated)
            //    {
            //        //TODO: Update our session list. 

            //    }
            //    await next.Invoke();
            //});

            app.UseMvc();

        }

        // Entry point for the application.
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
