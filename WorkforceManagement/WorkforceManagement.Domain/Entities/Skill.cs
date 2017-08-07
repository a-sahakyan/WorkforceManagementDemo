using System;
using System.Collections.Generic;
using System.Text;

namespace WorkforceManagement.Domain.Entities
{
   public class Skill
    {
        public int SkillId { get; set; }

        public int EmployeeId { get; set; }

        public string SkillName { get; set; }

        public string SkillKnowledge { get; set; }
    }
}
