using Microsoft.AspNetCore.Mvc;
using System;
using WorkforceManagement.BLL.Logic;
using WorkforceManagement.DDD.Models;
using WorkforceManagement.Domain.Entities;
using WorkforceManagement.VM.ViewModels;

namespace WorkforceManagement.WebUI.Controllers
{
    public class AccountController : Controller, IDisposable
    {
        IAuthenticationLogic _authentication;
        IAdminLogic _admLogic;

        public AccountController(IAuthenticationLogic authentication, IMapLogic<Employee, EmployeeDdd> employeeDtoMap,
            IAdminLogic admLogic)
        {
            _authentication = authentication;
            _admLogic = admLogic;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(EmployeeViewModel employee, AuthDataViewModel authData)
        {
            if (ModelState.IsValid)
            {
                AuthenticationLogic.IsAuthenticated = true;
                _authentication.SetAuthentication(AuthenticationLogic.IsAuthenticated);
                _authentication.Register(employee, authData);

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
        public IActionResult Login(AuthDataViewModel data)
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

            return RedirectToAction("Index", "Home");
        }
    }
}