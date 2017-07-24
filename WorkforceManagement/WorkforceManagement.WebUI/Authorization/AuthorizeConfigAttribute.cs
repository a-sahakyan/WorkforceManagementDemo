using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkforceManagement.WebUI.Authorization
{
    public class AuthorizeConfigAttribute : AuthorizeAttribute
    {
        public static string AutorizeAttr { get; set; }
        public AuthorizeConfigAttribute(params string[] roles) : base()
        {
            Roles = AutorizeAttr = string.Join(",", roles);
            
        }
    }
}
