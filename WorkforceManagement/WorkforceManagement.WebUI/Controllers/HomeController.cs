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

namespace WorkforceManagement.WebUI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        IRepository<Employee> _employee;
        IRepository<global::WorkforceManagement.Domain.Entities.AuthData> _authorization;
        private readonly EFDbContext _context;

        public HomeController(EFDbContext context)
        {
            _context = context;
            _employee = new EFModelContext<Employee>(_context);
            _authorization = new EFModelContext<global::WorkforceManagement.Domain.Entities.AuthData>(_context);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewBag.IsAuthenticated = false;
            object o = _employee.Model;
            foreach (var item in _authorization.Model)
            {

            }
            return View(o);
        }

        public IActionResult Index(Employee employee, AuthData authData)
        {
            //SqlConnection con = new SqlConnection();
            //SqlCommand cmd = new SqlCommand("SET IDENTITY_INSERT [dbo].[MyUser] ON", con);
            //cmd.CommandText = "aaa";

            //int retVal = cmd.ExecuteNonQuery();
            //_context.SaveChanges();

            //_context.Database.BeginTransaction();
            int id = _employee.Model.Select(x => x.EmployeeId).Last();

            _employee.Model = new List<Employee>()
            {
                new Employee() {Name = employee.Name,LastName=employee.LastName,Birth=employee.Birth,Profession=employee.Profession}
            };

            _authorization.Model = new List<AuthData>()
            {
                new AuthData() { Email = authData.Email,Password=authData.Password,Roles="User"}
            };


            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity("Cookie"));

            ViewBag.IsAuthenticated = userPrincipal.Identity.IsAuthenticated;

            return View();
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