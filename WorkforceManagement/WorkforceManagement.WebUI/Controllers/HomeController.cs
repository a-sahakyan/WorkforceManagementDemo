using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WorkforceManagement.BLL.Logic;
using WorkforceManagement.DAL.DataProvider;
using WorkforceManagement.DDD.Models;
using WorkforceManagement.Domain.Entities;
using WorkforceManagement.VM.ViewModels;

namespace WorkforceManagement.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IMapLogic<Skill, SkillViewModel> _mapperSkill;
        private IAuthenticationLogic _auth;
        private ISkillLogic _skill;

        public HomeController(IAuthenticationLogic auth, ISkillLogic skill,
            IMapLogic<Skill, SkillViewModel> mapperSkill)
        {
            _mapperSkill = mapperSkill;
            _auth = auth;
            _skill = skill;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var skillViewModel = _mapperSkill.MapAll();
            ViewBag.IsAuthenticated = AuthenticationLogic.IsAuthenticated;
            ViewBag.CurrentUser = AuthenticationLogic.CurrentUserId;
            _auth.SetAuthentication(AuthenticationLogic.IsAuthenticated);

            var content = DataAccess.LoadData("data");
            var data = JsonConvert.DeserializeObject<SkillConfig>(content);
            data.Count = "0";
            string json = JsonConvert.SerializeObject(data);
            DataAccess.WriteData("data", json);

            return View(skillViewModel);
        }

        [HttpPost]
        public IActionResult Index([FromBody]SkillViewModel datas)
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