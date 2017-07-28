using System;
using System.Collections.Generic;
using System.Text;

namespace WorkforceManagement.Domain.Entities
{
    public class Roles
    {
        public int RolesId { get; set; }
        public string Name { get; set; }
        public int PriviliegeId { get; set; }
    }
}
