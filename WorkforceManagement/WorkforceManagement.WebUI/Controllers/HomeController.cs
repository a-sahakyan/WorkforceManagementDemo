using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using WorkforceManagement.BLL.Logic;
using WorkforceManagement.DAL.DataProvider;
using WorkforceManagement.Domain.Entities;
using WorkforceManagement.DTO.Models;

namespace WorkforceManagement.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IMapLogic<Employee, EmployeeDto> _mapperEmployee;
        private IMapLogic<Skill, SkillDto> _mapperSkill;
        private IAuthenticationLogic _auth;
        private ISkillLogic _skill;

        public HomeController(IMapLogic<Employee, EmployeeDto> mapperEmployee, IAuthenticationLogic auth, ISkillLogic skill,
            IMapLogic<Skill, SkillDto> mapperSkill)
        {
            _mapperEmployee = mapperEmployee;
            _mapperSkill = mapperSkill;
            _auth = auth;
            _skill = skill;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employeeDto = _mapperEmployee.MapAll();
            var skillDto = _mapperSkill.MapAll();
            ViewBag.IsAuthenticated = AuthenticationLogic.IsAuthenticated;
            ViewBag.CurrentUser = AuthenticationLogic.CurrentUserId;
            _auth.SetAuthentication(AuthenticationLogic.IsAuthenticated);

            var content = DataAccess.LoadData("data");
            var data = JsonConvert.DeserializeObject<SkillConfig>(content);
            data.Count = "0";
            string json = JsonConvert.SerializeObject(data);
            DataAccess.WriteData("data", json);

            return View(skillDto);
        }

        [HttpPost]
        public IActionResult Index([FromBody]SkillDto datas)
        {
            _skill.SaveSkills(datas);

            return Json(datas);
        }

        public IActionResult JsonConfig([FromBody]SkillConfig data)
        {
            string json = JsonConvert.SerializeObject(data);
            DataAccess.WriteData("data", json);

            return Json(data);
        }
    }
}