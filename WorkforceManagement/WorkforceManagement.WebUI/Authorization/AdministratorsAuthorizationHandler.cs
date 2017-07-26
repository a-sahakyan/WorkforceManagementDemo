using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkforceManagement.Domain.Entities;

namespace WorkforceManagement.WebUI.Authorization
{
    public class AdministratorsAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, EmployeeModel>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, EmployeeModel resource)
        {
            if (context.User == null)
            {
                return Task.FromResult(0);
            }

            // Administrators can do anything.
            if (context.User.IsInRole("administrator"))
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}
