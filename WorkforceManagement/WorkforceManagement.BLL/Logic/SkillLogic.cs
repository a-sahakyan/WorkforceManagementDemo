using System.Collections.Generic;
using System.Linq;
using WorkforceManagement.DAL.DataProvider;
using WorkforceManagement.Domain.Entities;
using WorkforceManagement.VM.ViewModels;

namespace WorkforceManagement.BLL.Logic
{
    public class SkillLogic : ISkillLogic
    {
        IRepository<Skill> _skill;
        IMapLogic<Skill, SkillViewModel> _mapperSkill;

        public static int SkillCount { get; set; }

        public SkillLogic(IRepository<Skill> skill, IMapLogic<Skill, SkillViewModel> mapperSkill)
        {
            _skill = skill;
            _mapperSkill = mapperSkill;
        }

        public void SaveSkills(SkillViewModel datas)
        {
            datas.EmployeeId = AuthenticationLogic.CurrentUserId;
            var map = _mapperSkill.Map(datas);

            _skill.Insert(map);
        }

        public void UpdateSkills(SkillViewModel skills)
        {
            List<Skill> allSkilles = _skill.GetAll().ToList();
            foreach (var item in allSkilles)
            {
                var s = _mapperSkill.Map(skills);

                _skill.Update(s, item);
            }
        }

        public bool Check(SkillViewModel skill)
        {
            skill.EmployeeId = AuthenticationLogic.CurrentUserId;
            bool newSkill = true;
            List<Skill> allSkilles = _skill.GetAll().ToList();
            foreach (var item in allSkilles)
            {
                if (item.SkillName == skill.SkillName)
                {
                    newSkill = false;
                    break;
                }
            }

            return newSkill;
        }
    }
}
