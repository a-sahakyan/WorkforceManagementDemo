namespace WorkforceManagement.DDD.Models
{
        public class AuthDataDdd
        {
            public int EmployeeId { get; set; }

            public string Roles { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }

            public string ConfirmPassword { get; set; }
        }
}
