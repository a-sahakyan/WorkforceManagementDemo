using System;
using System.Collections.Generic;
using System.Text;
using WorkforceManagement.BLL.DataProvider;
using WorkforceManagement.DAL.Abstract;
using WorkforceManagement.Domain.Entities;
using WorkforceManagement.BLL.Authentication;
using System.Linq;


namespace WorkforceManagement.BLL.Authentication
{
    public class AuthenticationConfig : IAuthenticationConfig
    {
        public static bool IsAuthenticated { get; set; }

        public void Register(IDataPresenter<Employee> _employee, Employee employee, IDataPresenter<AuthData> _authData, AuthData authData)
        {
            _employee.DataHolder = new List<Employee>()
                {
                    new Employee() {Name = employee.Name,LastName=employee.LastName,Birth=employee.Birth,Profession=employee.Profession}
                };

            int id = _employee.DataHolder.Select(x => x.EmployeeId).Last();

            _authData.DataHolder = new List<AuthData>()
                {
                    new AuthData() {EmployeeId=id, Email = authData.Email,Password=authData.Password,Roles="User"}
                };
        }

        public string SignIn(IDataPresenter<AuthData> _authData,AuthData authData)
        {
            var adminEmail = _authData.DataHolder.Select(x => x.Email).First();
            var adminPass = _authData.DataHolder.Select(x => x.Password).First();
            string role = "";

            if (authData.Email == adminEmail && authData.Password == adminPass)
            {
                role = "admin";
            }
            else
            {
                foreach (var item in _authData.DataHolder)
                {
                    if (item.Email == authData.Email && item.Password == authData.Password)
                    {
                        AuthenticationConfig.IsAuthenticated = true;
                        role = "user";
                        break;
                    }
                }
            }

            return role;
        }
    }
}
