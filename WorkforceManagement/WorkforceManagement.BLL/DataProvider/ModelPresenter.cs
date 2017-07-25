using System;
using System.Collections.Generic;
using System.Text;
using WorkforceManagement.DAL.Abstract;
using WorkforceManagement.DAL.Concrete;
using WorkforceManagement.Domain.Entities;

namespace WorkforceManagement.BLL.DataProvider
{
    public class ModelPresenter<TModel> : IRepository<TModel> where TModel : class
    {
        public IEnumerable<TModel> DataPresenter
        {
            get
            {
                return ContextAccessor.Context.Set<TModel>();
            }
            set
            {
                foreach (var item in value)
                {
                    ContextAccessor.Context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                ContextAccessor.Context.SaveChanges();
            }
        }
    }
}
