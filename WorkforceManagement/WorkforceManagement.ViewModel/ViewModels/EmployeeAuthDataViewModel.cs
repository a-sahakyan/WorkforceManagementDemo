using System;
using System.Collections.Generic;
using System.Text;

namespace WorkforceManagement.ViewModel.ViewModels
{
    public class EmployeeAuthDataViewModel
    {
        public int EmployeeModelId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public DateTime Birth { get; set; }

        public string Profession { get; set; }
    }
}
