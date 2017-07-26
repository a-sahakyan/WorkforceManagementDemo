using System;
using System.Collections.Generic;
using System.Text;
using WorkforceManagement.BLL.DataProvider;
using WorkforceManagement.Domain.Entities;

namespace WorkforceManagement.BLL.Authentication
{
    public interface IAuthenticationConfig
    {
        void Register(IDataPresenter<EmployeeModel> _employee, EmployeeModel employee, IDataPresenter<AuthDataModel> _authData, AuthDataModel authData);

        string SignIn(IDataPresenter<AuthDataModel> _authData, AuthDataModel authData);
    }
}
