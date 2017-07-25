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
using WorkforceManagement.DAL.Abstract;

namespace WorkforceManagement.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Employee> _employee = new ModelPresenter<Employee>();
        IRepository<AuthData> _authData = new ModelPresenter<AuthData>();

        [HttpGet]
        //[AllowAnonymous]
        //[AuthorizeConfig("Admin")]
        //[Authorize(Roles = "aaa")]
        public IActionResult Index()
        {
            ViewBag.IsAuthenticated = false;
            ViewBag.IsAuthenticated = AuthorizationConfig.IsAuthenticated;

            return View(_employee.DataPresenter);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthPage()
        {
            return View();
        }
    }
}