using System;
using System.Collections.Generic;
using System.Text;

namespace WorkforceManagement.DTO.Models
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public DateTime Birth { get; set; }

        public string Profession { get; set; }
    }
}
