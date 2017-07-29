using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkforceManagement.BLL.Authentication;
using WorkforceManagement.BLL.Logic;
using WorkforceManagement.Domain.Entities;
using WorkforceManagement.DTO.Models;

namespace WorkforceManagement.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IMapLogic<Employee,EmployeeDto> _mapper;
        
        public HomeController(IMapLogic<Employee,EmployeeDto> mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        //[AllowAnonymous]
        //[AuthorizeConfig("Admin")]
        public IActionResult Index()
        {
            var employeeDto = _mapper.MapEntity();
            ViewBag.IsAuthenticated = false;
            ViewBag.IsAuthenticated = AuthenticationConfig.IsAuthenticated;

            return View(employeeDto);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthPage()
        {
            return View();
        }
    }
}