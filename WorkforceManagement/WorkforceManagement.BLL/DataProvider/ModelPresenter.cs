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

        public ModelPresenter() { }
        

        public IEnumerable<TModel> DataPresenter
        {
            get
            {
                return _context.Set<TModel>();
            }
            set
            {
                foreach (var item in value)
                {
                    _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                _context.SaveChanges();
            }
        }
    }
}
