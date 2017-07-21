using System;
using System.Collections.Generic;
using System.Text;

namespace WorkforceManagement.Domain.Entities
{
    public class AuthData
    {
        public int EmployeeId { get; set; }

        public int AuthDataId { get; set; }
        public string Roles { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual Employee Employess { get; set; }
    }
}
