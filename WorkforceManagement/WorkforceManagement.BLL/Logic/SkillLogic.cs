using System;
using System.Collections.Generic;
using System.Text;
using WorkforceManagement.DAL.DataProvider;
using WorkforceManagement.Domain.Entities;
using WorkforceManagement.DTO.Models;

namespace WorkforceManagement.BLL.Logic
{
    public class SkillLogic : ISkillLogic
    {
        IRepository<Skill> _skill;
        IMapLogic<Skill,SkillDto> _mapperSkill;

        public SkillLogic(IRepository<Skill> skill,IMapLogic<Skill,SkillDto> mapperSkill)
        {
            _skill = skill;
            _mapperSkill = mapperSkill;
        }

        public void SaveSkills(SkillDto datas)
        {
            datas.EmployeeId = AuthenticationLogic.CurrentUserId;
            var map = _mapperSkill.Map(datas);

            _skill.Insert(map);
        }
    }
}
