using System;
using System.Collections.Generic;
using System.Text;
using WorkforceManagement.DAL.Concrete;

namespace WorkforceManagement.DAL.DataProvider
{
    public class Repository<TModel> : IRepository<TModel> where TModel : class
    {
        EFDbContext _context;

        public Repository(EFDbContext context)
        {
            _context = context;
        }

        public TModel GetById => throw new NotImplementedException();

        public IEnumerable<TModel> Get
        {
            get { return _context.Set<TModel>(); }
        }

        public void Delete(TModel data)
        {
            throw new NotImplementedException();
        }

        public void Insert(TModel data)
        {
            _context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _context.SaveChanges();
        }

        public void Update(TModel data)
        {
            throw new NotImplementedException();
        }
    }
}
