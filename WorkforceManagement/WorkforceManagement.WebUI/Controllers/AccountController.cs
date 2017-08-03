using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Text;
using WorkforceManagement.BLL.Logic;
using WorkforceManagement.Domain.Entities;
using WorkforceManagement.DTO.Models;

namespace WorkforceManagement.WebUI.Controllers
{
    public class AccountController : Controller, IDisposable
    {
        IMapLogic<Employee, EmployeeDto> _employeeDtoMap;
        IAuthenticationLogic _authentication;
        IAdminLogic _admLogic;

        public AccountController(IAuthenticationLogic authentication, IMapLogic<Employee, EmployeeDto> employeeDtoMap,
            IAdminLogic admLogic, IHttpContextAccessor httpContextAccessor)
        {
            _employeeDtoMap = employeeDtoMap;
            _authentication = authentication;
            _admLogic = admLogic;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(EmployeeDto employee, AuthDataDto authData)
        {
            if (ModelState.IsValid)
            {
                AuthenticationLogic.IsAuthenticated = true;
                _authentication.SetAuthentication(AuthenticationLogic.IsAuthenticated);
                _authentication.Register(employee, authData);

                var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity("Cookie"));

                //AuthenticationLogic.IsAuthenticated = userPrincipal.Identity.IsAuthenticated;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Registration");
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AuthData data)
        {


            string role = _authentication.SignIn(data);


            if (role == "admin")
            {
                AuthenticationLogic.IsAuthenticated = true;
                _authentication.SetAuthentication(AuthenticationLogic.IsAuthenticated);
                return RedirectToAction("Admin");
            }
            else
            {
                if (role == "user")
                {
                    AuthenticationLogic.IsAuthenticated = true;
                    _authentication.SetAuthentication(AuthenticationLogic.IsAuthenticated);
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.ErrMsg = "invalid email or password";
                return View("Login");
            }
        }

        public IActionResult Admin()
        {
            _authentication.SetAuthentication(true);
            var data = _admLogic.GetAllUsersData();

            return View(data);
        }

        public IActionResult Logout()
        {
            AuthenticationLogic.IsAuthenticated = false;
            _authentication.SetAuthentication(AuthenticationLogic.IsAuthenticated);
            //AuthenticationLogic.IsAuthenticated = false;

            return RedirectToAction("Index", "Home");
        }
    }
}