using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorkforceManagement.Domain.Entities
{
    public class AuthDataModel
    {
        public int EmployeeId { get; set; }

        public int AuthDataModelId { get; set; }
        public string Roles { get; set; }

        [Required(ErrorMessage ="Please enter your email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please enter your password")]
        public string Password { get; set; }

        public virtual EmployeeModel Employees { get; set; }
    }
}
