using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkforceManagement.BLL.Logic;
using WorkforceManagement.Domain.Entities;
using WorkforceManagement.DTO.Models;

namespace WorkforceManagement.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IMapLogic<Employee, EmployeeDto> _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private IAuthenticationLogic _auth;

        public HomeController(IMapLogic<Employee, EmployeeDto> mapper, IHttpContextAccessor httpContextAccessor,IAuthenticationLogic auth)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _auth = auth;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employeeDto = _mapper.MapAll();
            ViewBag.IsAuthenticated = false;
            ViewBag.IsAuthenticated = AuthenticationLogic.IsAuthenticated;
            _auth.SetAuthentication(AuthenticationLogic.IsAuthenticated);

            return View(employeeDto);
        }
    }
}