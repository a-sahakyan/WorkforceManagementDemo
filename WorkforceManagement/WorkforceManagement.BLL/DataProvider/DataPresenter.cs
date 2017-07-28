using System;
using System.Collections.Generic;
using System.Text;
using WorkforceManagement.DAL.DataProvider;
using WorkforceManagement.Domain.Entities;

namespace WorkforceManagement.BLL.DataProvider
{
    public class DataPresenter<TModel> : IDataPresenter<TModel> where TModel : class
    {
        IRepository<TModel> _data;

        public DataPresenter(IRepository<TModel> data)
        {
            _data = data;
        }

        IRepository<TModel> IDataPresenter<TModel>.Data
        {
            get { return _data; }
        }
    }
}
