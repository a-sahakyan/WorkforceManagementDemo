using WorkforceManagement.VM.ViewModels;

namespace WorkforceManagement.BLL.Logic
{
    public interface IAuthenticationLogic
    {
        void Register(EmployeeViewModel employee, AuthDataViewModel authData);

        string SignIn(AuthDataViewModel authData);

        void SetAuthentication(bool isAuthenticated);
    }
}
