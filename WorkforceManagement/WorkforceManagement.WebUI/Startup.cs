using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using WorkforceManagement.WebUI.Authorization;
using Microsoft.EntityFrameworkCore;
using WorkforceManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WorkforceManagement.DAL.Concrete;
using WorkforceManagement.BLL.DataProvider;
using WorkforceManagement.DAL.Abstract;

namespace WorkforceManagement.WebUI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IHostingEnvironment env)
        {
            _contentRootPath = env.ContentRootPath;

            var builder = new ConfigurationBuilder()
                   .SetBasePath(env.ContentRootPath)
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                   .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        private string _contentRootPath = "";     //field

        string _testSecret = null;
        public void ConfigureServices(IServiceCollection services)
        {
            string conn = Configuration.GetConnectionString("DefaultConnection");
            if (conn.Contains("%CONTENTROOTPATH%"))
            {
                conn = conn.Replace("%CONTENTROOTPATH%", _contentRootPath);
            }
            services.AddDbContext<EFDbContext>(options =>
            {
                options.UseSqlServer(conn);
            });


            //    services.AddIdentity<ApplicationUser, IdentityRole>()
            //.AddEntityFrameworkStores<ApplicationDbContext>()
            //.AddDefaultTokenProviders();


            //services.AddMvc(config =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //                        .RequireAuthenticatedUser()
            //                        .Build();

            //    config.Filters.Add(new AuthorizeFilter(policy));
            //});
            //services.Add(new ServiceDescriptor(typeof(IRepository<Employee>), typeof(ModelPresenter<Employee>), ServiceLifetime.Transient));
            services.AddTransient<IRepository<Employee>, ModelPresenter<Employee>>();
            //services.AddTransient<IDataPresenter<Employee>, DataPresenter<Employee>>();
            services.AddTransient<IDataPresenter<Employee>, DataProcessor<Employee>>();
            services.AddMvc();

            //services.AddSingleton<IRepository<Employee>, ModelPresenter<Employee>>();


            //await CreateRoles(serviceProvider);


            //services.AddSingleton<IAuthorizationHandler, AdministratorsAuthorizationHandler>();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, EFDbContext context,IServiceProvider serviceProvider)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationScheme = "Cookie",
            //    LoginPath = new PathString("/Account/Login"),
            //    AccessDeniedPath = new PathString("/Account/Forbidden"),
            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true
            //});

            app.UseStaticFiles();

            //var _testUserPw = Configuration["SeedUserPW"];


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            DbInitalizer.Initalize(context);
        }


        //private async Task CreateRoles(IServiceProvider serviceProvider)
        //{
        //    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityRole>>();
        //    string[] roleNames = { "Admin", "Member" };
        //    IdentityResult roleResult;

        //    foreach (var roleName in roleNames)
        //    {
        //        var roleExist = await roleManager.RoleExistsAsync(roleName);
        //        if (!roleExist)
        //        {
        //            roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
        //        }
        //    }

        //    //creating a super user who could maintain the web app
        //    var powerUser = new ApplicationUser
        //    {
        //        UserName = Configuration.GetSection("UserSettings")["UserEmail"],
        //        Email = Configuration.GetSection("UserSettings")["UserEmail"]
        //    };

        //    string userPassword = Configuration.GetSection("UserSettings")["UserPassword"];



        //    var _user = await userManager.FindByEmailAsync(Configuration.GetSection("UserSettings")["UserEmail"]);

        //    if (_user == null)
        //    {
        //        var createPowerUser = await userManager.CreateAsync(powerUser, userPassword);
        //        if (createPowerUser.Succeeded)
        //        {
        //            // here we tie the new user to the "Admin" role

        //            await userManager.AddToRoleAsync(powerUser, "Admin");
        //        }
        //    }
        //}
    }

}
