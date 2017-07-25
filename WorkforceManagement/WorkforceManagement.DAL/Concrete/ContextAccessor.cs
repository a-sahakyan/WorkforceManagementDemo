using System;
using System.Collections.Generic;
using System.Text;

namespace WorkforceManagement.DAL.Concrete
{
    public class ContextAccessor
    {
        private static EFDbContext _context;

        public ContextAccessor(EFDbContext context)
        {
            _context = context;
        }

        public static EFDbContext Context
        {
            get { return _context; }
        }
    }
}
