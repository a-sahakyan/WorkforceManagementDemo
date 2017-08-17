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

            return View(skillViewModel);
        }

        [HttpPost]
        public IActionResult Index([FromBody]SkillViewModel datas)
        {
            if (datas.SkillName != "")
            {
                var newSkill = _skill.Check(datas);
                if (newSkill)
                {
                    _skill.SaveSkills(datas);
                }
                else
                {
                    _skill.UpdateSkills(datas);
                }
            }

            return Json(datas);
        }
    }
}