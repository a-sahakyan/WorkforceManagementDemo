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
using WorkforceManagement.BLL.Authentication;
using AutoMapper;
using WorkforceManagement.ViewModel.ViewModels;

namespace WorkforceManagement.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IDataPresenter<EmployeeModel> _employee;
        IDataPresenter<AuthDataModel> _authData;
        IMapper _mapper;

        public HomeController(IDataPresenter<EmployeeModel> employee,IDataPresenter<AuthDataModel> authData,IMapper mapper)
        {
            _employee = employee;
            _authData = authData;
            _mapper = mapper;
            //var u = _employee.DataHolder.First();

            //var model = _mapper.Map<EmployeeModel, EmployeeAuthDataViewModel>(u);
            //int a = 12;
        }

        [HttpGet]
        //[AllowAnonymous]
        //[AuthorizeConfig("Admin")]
        //[Authorize(Roles = "aaa")]
        public IActionResult Index()
        {
            ViewBag.IsAuthenticated = false;
            ViewBag.IsAuthenticated = AuthenticationConfig.IsAuthenticated;

            return View(_employee.DataHolder);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthPage()
        {
            return View();
        }
    }
}