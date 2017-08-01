using System.Collections.Generic;
using WorkforceManagement.VM.ViewModels;

namespace WorkforceManagement.BLL.Logic
{
    public interface IAdminLogic
    {
        IEnumerable<UserDataViewModel> GetAllUsersData();
    }
}
