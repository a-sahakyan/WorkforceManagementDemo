using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using WorkforceManagement.BLL.Authentication;
using WorkforceManagement.BLL.DataProvider;
using WorkforceManagement.BLL.Logic;
using WorkforceManagement.Domain.Entities;
using WorkforceManagement.DTO.Models;

namespace WorkforceManagement.WebUI.Controllers
{
    //[Authorize]
    public class AccountController : Controller, IDisposable
    {
        IMapLogic<Employee, EmployeeDto> _employeeDtoMap;
        IMapLogic<AuthData, AuthDataDto> _authDataMap;
        IAuthenticationConfig _authConfig;

        public AccountController(IAuthenticationConfig authConfig,IMapLogic<Employee,EmployeeDto> employeeDtoMap)
        {
            _employeeDtoMap = employeeDtoMap;
            _authConfig = authConfig;
        }

        public IActionResult Forbidden()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Registration(EmployeeDto employee, AuthDataDto authData)
        {
            if (ModelState.IsValid)
            {
                _authConfig.Register(employee,authData);

                var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity("Cookie"));

                AuthenticationConfig.IsAuthenticated = userPrincipal.Identity.IsAuthenticated;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Registration");
            }
        }

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> Login(AuthData data)
        //{
        //    var role = new IdentityRole();
        //    role.Name = "admin";
        //    var userFromStorage = TestUserStorage.UserList
        //        .FirstOrDefault(m => m. == userFromFore.Email && m.Password == userFromFore.Password);

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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(AuthData data)
        {
            string role = _authConfig.SignIn(data);

            if (role == "admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                if (role == "user")
                {
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.ErrMsg = "invalid email or password";
                return View("Login");
            }

            string userName = data.Email;
            //string[] userRoles = _roles.Model.Select(x => x.Name).ToArray();

            ClaimsIdentity identity = new ClaimsIdentity();
            ClaimsIdentity i = new ClaimsIdentity();

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userName));

            //userRoles.ToList().ForEach((role) => identity.AddClaim(new Claim(ClaimTypes.Role, role)));

            identity.AddClaim(new Claim(ClaimTypes.Name, userName));

            //AuthenticationManager.SignIn(identity);
        }

        //[Authorize(Roles = "Admin")]
        //[Authorize(Roles = "User")]
        public IActionResult Logout()
        {
            //await HttpContext.Authentication.SignOutAsync("Cookie");
            AuthenticationConfig.IsAuthenticated = false;

            return RedirectToAction("Index", "Home");
        }

        //public static class TestUserStorage
        //{
        //    public static List<AuthData> UserList { get; set; } = new List<AuthData>()
        //    {
        //       new Employee { Email = "User1",Password = "112233"}
        //    };
        //}
    }
}