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

        public async Task<IActionResult> Index(string email, string password, AuthData userFromFore)
        {
            //var userFromStorage = _authorization.Model.ToList()
            //    .FirstOrDefault(m => m.Email == userFromFore.Email && m.Password == userFromFore.Password);

            //if (userFromStorage == null)
            //{
            //    //you can add all of ClaimTypes in this collection 
            //    var claims = new List<Claim>()
            //    {
            //        new Claim(ClaimTypes.Name,userFromStorage.Email) 
            //        //,new Claim(ClaimTypes.Email,"emailaccount@microsoft.com")  
            //     };
                var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity("Cookie"));

                ViewBag.IsAuthenticated = userPrincipal.Identity.IsAuthenticated;

                
            //var userPrincipal = new ClaimsPrincipal("cookie");
            //await HttpContext.Authentication.SignInAsync("cookie", userPrincipal);
            //}
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