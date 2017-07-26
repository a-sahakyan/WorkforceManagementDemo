using System;
using System.Collections.Generic;
using System.Text;

namespace WorkforceManagement.DAL.Abstract
{
    public interface IRepository<TModel>  where TModel :class
    {
        IEnumerable<TModel> DataPresenter { get; set; }
    }
}
