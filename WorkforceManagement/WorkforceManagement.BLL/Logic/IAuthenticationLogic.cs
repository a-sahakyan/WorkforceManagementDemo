using WorkforceManagement.Domain.Entities;
using WorkforceManagement.DTO.Models;

namespace WorkforceManagement.BLL.Logic
{
    public interface IAuthenticationLogic
    {
        void Register(EmployeeDto employee, AuthDataDto authData);

        string SignIn(AuthData authData);

        void SetAuthentication(bool isAuthenticated);
    }
}
