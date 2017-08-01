using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            var employeeDto = _mapper.MapAll();
            ViewBag.IsAuthenticated = false;
            ViewBag.IsAuthenticated = AuthenticationLogic.IsAuthenticated;

            return View(employeeDto);
        }
    }
}