using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorkforceManagement.Domain.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [MaxLength(50,ErrorMessage ="Your name is too long")]
        [Required(ErrorMessage ="Please enter your name")]
        public string Name { get; set; }

        [MaxLength(50,ErrorMessage ="Your last name is too long")]
        [Required(ErrorMessage ="Please enter your last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Please enter your birth")]
        public DateTime Birth { get; set; }

        [Required(ErrorMessage = "Please enter your Profession")]
        public string Profession { get; set; }
        //public string Email { get; set; }
        //public string Password { get; set; }
    }
}
