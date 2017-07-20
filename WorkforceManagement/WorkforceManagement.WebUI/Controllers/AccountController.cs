using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkforceManagement.Domain.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;

namespace WorkforceManagement.WebUI.Controllers
{
    public class AccountController : Controller
    {


        public IActionResult Forbidden()
        {
            return View();
        }

        public async Task<IActionResult> Login(Employee userFromFore)
        {
            var userFromStorage = TestUserStorage.UserList
                .FirstOrDefault(m => m.Email == userFromFore.Email && m.Password == userFromFore.Password);

            if (userFromStorage != null)
            {
                //you can add all of ClaimTypes in this collection 
                var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name,userFromStorage.Email) 
            //,new Claim(ClaimTypes.Email,"emailaccount@microsoft.com")  
        };

                //init the identity instances 
                var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "SuperSecureLogin"));

                //signin 
                await HttpContext.Authentication.SignInAsync("Cookie", userPrincipal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    IsPersistent = false,
                    AllowRefresh = false
                });

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrMsg = "UserName or Password is invalid";

                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookie");

            return RedirectToAction("Index", "Home");
        }

        public static class TestUserStorage
        {
            public static List<Employee> UserList { get; set; } = new List<Employee>() {
            new Employee { Email = "User1",Password = "112233"}
        };
        }
        
    }
}