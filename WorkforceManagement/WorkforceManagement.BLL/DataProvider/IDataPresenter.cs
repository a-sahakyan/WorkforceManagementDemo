using System;
using System.Collections.Generic;
using System.Text;
using WorkforceManagement.DAL.Abstract;

namespace WorkforceManagement.BLL.DataProvider
{
    public interface IDataPresenter<TModel> where TModel : class
    {
        IEnumerable<TModel> DataHolder { get; set; }
    }
}
