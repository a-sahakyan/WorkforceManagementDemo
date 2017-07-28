using System;
using System.Collections.Generic;
using System.Text;
using WorkforceManagement.BLL.DataProvider;
using WorkforceManagement.Domain.Entities;
using System.Linq;
using AutoMapper;
using WorkforceManagement.DTO.Models;

namespace WorkforceManagement.BLL.Authentication
{
    public class AuthenticationConfig : Profile, IAuthenticationConfig
    {
        public static bool IsAuthenticated { get; set; }

        public void Register(IDataPresenter<Employee> _employee, Employee employee, IDataPresenter<AuthData> _authData, AuthData authData)
        {
           var a = Mapper.Map<EmployeeDto>(employee);

            _employee.Data.Insert(employee);

            int id = _employee.Data.Get.Select(x => x.EmployeeId).Last();
            authData.Roles = "User";
            authData.EmployeeId = id;

            _authData.Data.Insert(authData);
        }

        public string SignIn(IDataPresenter<AuthData> _authData,AuthData authData)
        {
            var adminEmail = _authData.Data.Get.Select(x => x.Email).First();
            var adminPass = _authData.Data.Get.Select(x => x.Password).First();
            string role = "";

            if (authData.Email == adminEmail && authData.Password == adminPass)
            {
                role = "admin";
            }
            else
            {
                foreach (var item in _authData.Data.Get)
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
