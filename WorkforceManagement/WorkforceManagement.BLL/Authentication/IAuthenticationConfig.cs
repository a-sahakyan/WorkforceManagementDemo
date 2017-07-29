using System;
using System.Collections.Generic;
using System.Text;
using WorkforceManagement.BLL.DataProvider;
using WorkforceManagement.DAL.DataProvider;
using WorkforceManagement.Domain.Entities;
using WorkforceManagement.DTO.Models;

namespace WorkforceManagement.BLL.Authentication
{
    public interface IAuthenticationConfig
    {
        void Register(EmployeeDto employee,AuthDataDto authData);

        string SignIn(AuthData authData);
    }
}
