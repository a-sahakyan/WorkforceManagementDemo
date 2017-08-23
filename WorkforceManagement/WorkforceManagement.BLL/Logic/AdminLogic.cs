using System.Collections.Generic;
using System.Linq;
using WorkforceManagement.DAL.DataProvider;
using WorkforceManagement.Domain.Entities;
using WorkforceManagement.VM.ViewModels;

namespace WorkforceManagement.BLL.Logic
{
    public class AdminLogic : IAdminLogic
    {
        IRepository<Employee> _employee;
        IRepository<AuthData> _authData;
        IMapLogic<Employee, UserDataViewModel> _mapUser;
        IMapLogic<AuthData, UserDataViewModel> _mapData;

        public AdminLogic(IMapLogic<Employee, UserDataViewModel> mapUser, IMapLogic<AuthData, UserDataViewModel> mapData,
            IRepository<Employee> employee, IRepository<AuthData> authData)
        {
            _employee = employee;
            _authData = authData;
            _mapUser = mapUser;
            _mapData = mapData;
        }

        public IEnumerable<UserDataViewModel> GetAllUsersData()
        {
            var userDatas = _mapUser.MapAll();
            int emailCount = 0;
            int passCount = 0;

            foreach (var item in userDatas)
            {
                item.Email = _authData.GetAll().Select(x => x.Email).Skip(emailCount).Take(++emailCount).First();
                item.Password = _authData.GetAll().Select(x => x.Password).Skip(passCount).Take(++passCount).First();
            }

            return userDatas;
        }
    }
}
