using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WorkforceManagement.Domain.Entities;
using Microsoft.AspNetCore.Http.Authentication;
using System.Security.Claims;
using System.Data.SqlClient;
using WorkforceManagement.WebUI.Authorization;
using WorkforceManagement.BLL.DataProvider;
using WorkforceManagement.BLL.Authentication;
using AutoMapper;
using WorkforceManagement.DTO.Models;

namespace WorkforceManagement.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IDataPresenter<Employee> _employee;
        IDataPresenter<AuthData> _authData;

        public HomeController(IDataPresenter<Employee> employee, IDataPresenter<AuthData> authData)
        {
            _employee = employee;
            _authData = authData;
        }

        [HttpGet]
        //[AllowAnonymous]
        //[AuthorizeConfig("Admin")]
        //[Authorize(Roles = "aaa")]
        public IActionResult Index()
        {
            var  model = AutoMapper.Mapper.Map<IEnumerable<EmployeeDto>>(_employee.Data.Get);
            ViewBag.IsAuthenticated = false;
            ViewBag.IsAuthenticated = AuthenticationConfig.IsAuthenticated;

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthPage()
        {
            return View();
        }
    }
}