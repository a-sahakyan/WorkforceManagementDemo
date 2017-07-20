using System;
using System.Collections.Generic;
using System.Text;
using WorkforceManagement.Domain.Abstract;

namespace WorkforceManagement.Domain.Concrete
{
    public class DbInitalizer<TModel> : IRepository<TModel> where TModel : class
    {
        private readonly EFDbContext _context;
        private IEnumerable<TModel> _model;

        public DbInitalizer(EFDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TModel> Model
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
            }
        }

        public static void Initalize(EFDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
