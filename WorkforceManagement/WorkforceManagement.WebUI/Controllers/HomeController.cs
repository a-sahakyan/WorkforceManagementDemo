using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using WorkforceManagement.BLL.Logic;
using WorkforceManagement.Domain.Entities;
using WorkforceManagement.DTO.Models;

namespace WorkforceManagement.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IMapLogic<Employee, EmployeeDto> _mapper;
        private IAuthenticationLogic _auth;

        public HomeController(IMapLogic<Employee, EmployeeDto> mapper, IHttpContextAccessor httpContextAccessor, IAuthenticationLogic auth)
        {
            _mapper = mapper;
            _auth = auth;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employeeDto = _mapper.MapAll();
            ViewBag.IsAuthenticated = AuthenticationLogic.IsAuthenticated;
            _auth.SetAuthentication(AuthenticationLogic.IsAuthenticated);

            return View(employeeDto);
        }

        public IActionResult Index([FromBody]SkillDto datas)
        {
            int a = 24;

            return Json(datas);
        }

        public IActionResult JsonConfig([FromBody]CountDto data)
        {
            string path = @"wwwroot\js\data.json";
            string json = JsonConvert.SerializeObject(data);
            using (FileStream fs = new FileStream(path,FileMode.Truncate))
            using (StreamWriter sr = new StreamWriter(fs))
            {
                sr.Write(json);
            }

            return Json(data);
        }

    }

    public class CountDto
    {
        public string Count { get; set; }
    }
}