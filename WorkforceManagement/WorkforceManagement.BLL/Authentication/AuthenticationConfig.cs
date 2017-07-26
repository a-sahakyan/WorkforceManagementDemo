using System;
using System.Collections.Generic;
using System.Text;
using WorkforceManagement.BLL.DataProvider;
using WorkforceManagement.Domain.Entities;
using System.Linq;


namespace WorkforceManagement.BLL.Authentication
{
    public class AuthenticationConfig : IAuthenticationConfig
    {
        public static bool IsAuthenticated { get; set; }

        public void Register(IDataPresenter<EmployeeModel> _employee, EmployeeModel employee, IDataPresenter<AuthDataModel> _authData, AuthDataModel authData)
        {
            _employee.DataHolder = new List<EmployeeModel>()
                {
                    new EmployeeModel() {Name = employee.Name,LastName=employee.LastName,Birth=employee.Birth,Profession=employee.Profession}
                };

            int id = _employee.DataHolder.Select(x => x.EmployeeModelId).Last();

            _authData.DataHolder = new List<AuthDataModel>()
                {
                    new AuthDataModel() {EmployeeId=id, Email = authData.Email,Password=authData.Password,Roles="User"}
                };
        }

        public string SignIn(IDataPresenter<AuthDataModel> _authData,AuthDataModel authData)
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
