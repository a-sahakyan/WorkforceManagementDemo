using System;
using System.Collections.Generic;
using System.Text;

namespace WorkforceManagement.Domain.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime JoinDate { get; set; }
        public string Profession { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
