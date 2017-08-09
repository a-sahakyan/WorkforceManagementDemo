using WorkforceManagement.DAL.DataProvider;
using WorkforceManagement.DDD.Models;
using WorkforceManagement.Domain.Entities;
using WorkforceManagement.VM.ViewModels;

namespace WorkforceManagement.BLL.Logic
{
    public class SkillLogic : ISkillLogic
    {
        IRepository<Skill> _skill;
        IMapLogic<Skill,SkillViewModel> _mapperSkill;

        public SkillLogic(IRepository<Skill> skill,IMapLogic<Skill,SkillViewModel> mapperSkill)
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
    }
}
