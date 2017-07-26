using System;
using System.Collections.Generic;
using System.Text;
using WorkforceManagement.BLL.DataProvider;
using WorkforceManagement.Domain.Entities;

namespace WorkforceManagement.BLL.Authentication
{
    public interface IAuthenticationConfig
    {
        void Register(IDataPresenter<Employee> _employee, Employee employee, IDataPresenter<AuthData> _authData, AuthData authData);

        string SignIn(IDataPresenter<AuthData> _authData, AuthData authData);
    }
}
