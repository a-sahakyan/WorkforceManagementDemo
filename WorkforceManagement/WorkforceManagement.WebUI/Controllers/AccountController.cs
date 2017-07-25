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
using WorkforceManagement.WebUI.Authorization;
using WorkforceManagement.BLL.DataProvider;
using WorkforceManagement.DAL.Abstract;

namespace WorkforceManagement.WebUI.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        IRepository<Employee> _employee = new ModelPresenter<Employee>();
        IRepository<AuthData> _authData = new ModelPresenter<AuthData>();

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
            var adminEmail = _authData.DataPresenter.Select(x => x.Email).First();
            var adminPass = _authData.DataPresenter.Select(x => x.Password).First();
            AuthorizeAttribute auth = new AuthorizeAttribute();
            auth.Roles = _authData.DataPresenter.Select(x => x.Roles).First();
            AuthorizeConfigAttribute.AutorizeAttr = _authData.DataPresenter.Select(x => x.Roles).First();

            new List<int> { 1, 2 }.TakeWhile(x => x == 7);

            if (data.Email == adminEmail && data.Password == adminPass)
            {


                foreach (var item in auth.Roles)
                {

                }
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                foreach (var item in _authData.DataPresenter)
                {
                    if (item.Email == data.Email && item.Password == data.Password)
                    {
                        AuthorizationConfig.IsAuthenticated = true;
                        ViewBag.Hello = _authData.DataPresenter.Select(x => x.Employess.Name);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, "invalid email or password");
                return View("Login");
            }


            string userName = data.Email;
            string[] userRoles = _roles.Model.Select(x => x.Name).ToArray();

            ClaimsIdentity identity = new ClaimsIdentity();
            ClaimsIdentity i = new ClaimsIdentity();

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userName));

            userRoles.ToList().ForEach((role) => identity.AddClaim(new Claim(ClaimTypes.Role, role)));

            identity.AddClaim(new Claim(ClaimTypes.Name, userName));

            //AuthenticationManager.SignIn(identity);
        }

        //[Authorize(Roles = "Admin")]
        //[Authorize(Roles = "User")]
        public IActionResult Logout()
        {
            //await HttpContext.Authentication.SignOutAsync("Cookie");
            AuthorizationConfig.IsAuthenticated = false;

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