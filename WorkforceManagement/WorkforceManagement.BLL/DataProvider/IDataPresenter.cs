using System;
using System.Collections.Generic;
using System.Text;
using WorkforceManagement.DAL.DataProvider;

namespace WorkforceManagement.BLL.DataProvider
{
    public interface IDataPresenter<TModel>  where TModel : class 
    {
        IRepository<TModel> Data { get; }
    }
}
