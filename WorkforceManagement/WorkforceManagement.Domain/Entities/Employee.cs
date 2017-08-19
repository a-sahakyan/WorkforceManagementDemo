using System;

namespace WorkforceManagement.Domain.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public DateTime Birth { get; set; }

        public string Profession { get; set; }
    }
}
