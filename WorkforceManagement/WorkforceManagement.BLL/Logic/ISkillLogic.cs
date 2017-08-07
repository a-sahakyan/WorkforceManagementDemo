using System;
using System.Collections.Generic;
using System.Text;
using WorkforceManagement.DTO.Models;

namespace WorkforceManagement.BLL.Logic
{
    public interface ISkillLogic
    {
        void SaveSkills(SkillDto datas);
    }
}
