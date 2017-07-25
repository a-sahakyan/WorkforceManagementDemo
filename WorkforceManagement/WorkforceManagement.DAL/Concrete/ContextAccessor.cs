using System;
using System.Collections.Generic;
using System.Text;

namespace WorkforceManagement.DAL.Concrete
{
    public static class ContextAccessor
    {
        private static EFDbContext _context;

        public static EFDbContext Context
        {
            get { return _context; }
            set { _context = value; }
        }
    }
}
