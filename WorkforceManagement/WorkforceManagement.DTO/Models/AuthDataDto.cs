using System;
using System.Collections.Generic;
using System.Text;

namespace WorkforceManagement.DTO.Models
{
    public class AuthDataDto
    {
        public int EmployeeId { get; set; }

        public string Roles { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
