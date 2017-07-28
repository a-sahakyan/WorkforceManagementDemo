using System;
using System.Collections.Generic;
using System.Text;
using WorkforceManagement.DAL.Abstract;
using WorkforceManagement.DAL.Concrete;
using WorkforceManagement.Domain.Entities;

namespace WorkforceManagement.BLL.DataProvider
{
    public class ModelPresenter<TModel> : IRepository<TModel> where TModel :class
    {
        EFDbContext _context;

        public ModelPresenter(EFDbContext context)
        {
            _context = context;
        }        

        public IEnumerable<TModel> DataPresenter
        {
            get
            {
                return _context.Set<TModel>();
            }
        }

        public TModel DataPusherConfig
        {
            set
            {
                _context.Entry(value).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                _context.SaveChanges();
            }

        }
    }
}
