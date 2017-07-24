using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkforceManagement.Domain.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WorkforceManagement.Domain.Abstract;
using WorkforceManagement.Domain.Concrete;
using WorkforceManagement.WebUI.Authorization;

namespace WorkforceManagement.WebUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly EFDbContext _context;
        IRepository<AuthData> _rep;
        IRepository<Employee> _employee;
        IRepository<global::WorkforceManagement.Domain.Entities.AuthData> _authorization;

        public AccountController(EFDbContext context)
        {
            _context = context;
            _employee = new EFModelContext<Employee>(_context);
            _authorization = new EFModelContext<AuthData>(_context);

        }

        public IActionResult Forbidden()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Index(Employee employee, AuthData authData)
        {
            if (ModelState.IsValid)
            {
                _employee.Model = new List<Employee>()
                {
                    new Employee() {Name = employee.Name,LastName=employee.LastName,Birth=employee.Birth,Profession=employee.Profession}
                };

                int id = _employee.Model.Select(x => x.EmployeeId).Last();

                _authorization.Model = new List<AuthData>()
                {
                    new AuthData() {EmployeeId=id, Email = authData.Email,Password=authData.Password,Roles="User"}
                };

                var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity("Cookie"));

                AuthorizationConfig.IsAuthenticated = userPrincipal.Identity.IsAuthenticated;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Registration");
            }
        }

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> Login(Employee userFromFore)
        //{
        //    var role = new IdentityRole();
        //    role.Name = "admin";
        //    //var userFromStorage = TestUserStorage.UserList
        //    //    .FirstOrDefault(m => m.Email == userFromFore.Email && m.Password == userFromFore.Password);

        //    if (userFromStorage != null)
        //    {
        //        //you can add all of ClaimTypes in this collection 
        //        var claims = new List<Claim>()
        //{
        //    new Claim(ClaimTypes.Name,userFromStorage.Email) 
        //    //,new Claim(ClaimTypes.Email,"emailaccount@microsoft.com")  
        //};

        //        //init the identity instances 
        //        var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "SuperSecureLogin"));

        //        //signin 
        //        await HttpContext.Authentication.SignInAsync("Cookie", userPrincipal, new AuthenticationProperties
        //        {
        //            ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
        //            IsPersistent = false,
        //            AllowRefresh = false
        //        });
        //        AuthorizeAttribute a = new AuthorizeAttribute();
        //        object o = a.Roles;


        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        ViewBag.ErrMsg = "UserName or Password is invalid";

        //        return View();
        //    }
        //}

        //[Authorize(Roles ="admin")]
        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.Authentication.SignOutAsync("Cookie");

        //    return RedirectToAction("Index", "Home");
        //}

        //public static class TestUserStorage
        //{
        //    public static List<Employee> UserList { get; set; } = new List<Employee>()
        //    {
        //       new Employee { Email = "User1",Password = "112233"}
        //    };
        //}

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Registration()
        {
            return View();
        }
    }
}