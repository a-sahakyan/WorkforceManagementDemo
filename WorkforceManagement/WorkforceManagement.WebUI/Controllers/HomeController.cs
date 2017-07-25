using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WorkforceManagement.Domain.Concrete;
using WorkforceManagement.Domain.Abstract;
using WorkforceManagement.Domain.Entities;
using Microsoft.AspNetCore.Http.Authentication;
using System.Security.Claims;
using System.Data.SqlClient;
using WorkforceManagement.WebUI.Authorization;

namespace WorkforceManagement.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Employee> _employee;
        IRepository<global::WorkforceManagement.Domain.Entities.AuthData> _authorization;
        private readonly EFDbContext _context;
        

        public HomeController(EFDbContext context)
        {
            AuthorizeConfigAttribute.AutorizeAttr = "Admin";
            _context = context;
            _employee = new EFModelContext<Employee>(_context);
            _authorization = new EFModelContext<global::WorkforceManagement.Domain.Entities.AuthData>(_context);
        }

        [HttpGet]
        //[AllowAnonymous]
        //[AuthorizeConfig("Admin")]
        //[Authorize(Roles = "aaa")]
        public IActionResult Index()
        {
            ViewBag.IsAuthenticated = false;
            object o = _employee.Model;
            foreach (var item in _authorization.Model)
            {

            }
            ViewBag.IsAuthenticated = AuthorizationConfig.IsAuthenticated;

            return View(o);
        }

       

        public void Check()
        {

        }

        public void SetRole()
        {
            AuthorizeAttribute role = new AuthorizeAttribute();
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthPage()
        {
            return View();
        }
    }
}