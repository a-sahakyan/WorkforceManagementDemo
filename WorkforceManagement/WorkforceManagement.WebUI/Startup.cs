using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using WorkforceManagement.BLL.Logic;
using WorkforceManagement.DAL.Concrete;
using WorkforceManagement.DAL.DataProvider;
using WorkforceManagement.Domain.Entities;
using WorkforceManagement.DTO.Models;
using WorkforceManagement.VM.ViewModels;

namespace WorkforceManagement.WebUI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        private string _contentRootPath = "";
        public IConfigurationRoot Configuration { get; }

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

            services.AddScoped<IRepository<Employee>, Repository<Employee>>();
            services.AddScoped<IRepository<AuthData>, Repository<AuthData>>();
            services.AddScoped<IRepository<Skill>, Repository<Skill>>();
            services.AddScoped<IMapLogic<Employee, EmployeeDdd>, MapLogic<Employee, EmployeeDdd>>();
            services.AddScoped<IMapLogic<AuthData, AuthDataDdd>, MapLogic<AuthData, AuthDataDdd>>();
            services.AddScoped<IMapLogic<Employee, UserDataViewModel>, MapLogic<Employee, UserDataViewModel>>();
            services.AddScoped<IMapLogic<AuthData, UserDataViewModel>, MapLogic<AuthData, UserDataViewModel>>();
            services.AddScoped<IMapLogic<Skill, SkillDdd>, MapLogic<Skill, SkillDdd>>();
            services.AddScoped<IAdminLogic, AdminLogic>();
            services.AddScoped<ISkillLogic, SkillLogic>();
            services.AddScoped<IAuthenticationLogic, AuthenticationLogic>();
            services.AddMvc();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, EFDbContext context, IServiceProvider serviceProvider)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async contextBuilder =>
                    {
                        contextBuilder.Response.StatusCode = 500;
                        await contextBuilder.Response.WriteAsync("An unexpected fault happend. Try again later.");
                    });
                });
            }

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDdd>();
                cfg.CreateMap<EmployeeDdd, Employee>();
                cfg.CreateMap<AuthDataDdd, AuthData>();
                cfg.CreateMap<Skill, SkillDdd>();
                cfg.CreateMap<SkillDdd, Skill>();
                cfg.CreateMap<Employee, UserDataViewModel>();
                cfg.CreateMap<AuthData, UserDataViewModel>().ForMember(src => src.Email, dest => dest.MapFrom(x => x.Email));
            });

            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            DbInitalizer.Initalize(context);
        }
    }
}
