using System;

namespace WorkforceManagement.VM.ViewModels
{
    public class UserDataViewModel
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public DateTime Birth { get; set; }

        public string Profession { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
