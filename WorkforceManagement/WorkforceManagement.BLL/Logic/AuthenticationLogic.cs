using Microsoft.AspNetCore.Http;
using System.Linq;
using WorkforceManagement.DAL.DataProvider;
using WorkforceManagement.Domain.Entities;
using WorkforceManagement.DTO.Models;

namespace WorkforceManagement.BLL.Logic
{
    public class AuthenticationLogic : IAuthenticationLogic
    {
        private IMapLogic<Employee, EmployeeDdd> _mapperEmployee;
        private IMapLogic<AuthData, AuthDataDdd> _mapperAuthData;
        private IRepository<Employee> _employee;
        private IRepository<AuthData> _authData;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public static bool IsAuthenticated { get; set; }

        public AuthenticationLogic(IMapLogic<Employee, EmployeeDdd> mapperEmployee, IMapLogic<AuthData, AuthDataDdd> mapperAuthData,
            IRepository<Employee> employee, IRepository<AuthData> authData,IHttpContextAccessor http)
        {
            _mapperEmployee = mapperEmployee;
            _mapperAuthData = mapperAuthData;
            _employee = employee;
            _authData = authData;
            _httpContextAccessor = http;
        }

        public void SetAuthentication(bool isAuthenticated)
        {
            _session.SetString("IsAuth", isAuthenticated.ToString());
        }

        public void Register(EmployeeDdd employee, AuthDataDdd authData)
        {
            var newEmployee = _mapperEmployee.Map(employee);
            _employee.Insert(newEmployee);

            int id = _employee.GetAll().Select(x => x.EmployeeId).Last();
            authData.Roles = "User";
            authData.EmployeeId = id;
            CurrentUserId = id;

            var newAuthData = _mapperAuthData.Map(authData);
            _authData.Insert(newAuthData);
        }

        public static int CurrentUserId { get; set; }

        public string SignIn(AuthData authData)
        {
            var adminEmail = _authData.GetAll().Select(x => x.Email).First();
            var adminPass = _authData.GetAll().Select(x => x.Password).First();
            string role = "";

            if (authData.Email == adminEmail && authData.Password == adminPass)
            {
                CurrentUserId = _authData.GetAll().Select(x=>x.EmployeeId).First();
                role = "admin";
            }
            else
            {
                foreach (var item in _authData.GetAll())
                {
                    if (item.Email == authData.Email && item.Password == authData.Password)
                    {
                        CurrentUserId = item.EmployeeId;
                        role = "user";
                        break;
                    }
                }
            }
            return role;
        }
    }
}

