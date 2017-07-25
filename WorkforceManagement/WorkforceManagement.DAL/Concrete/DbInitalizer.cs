using System;
using System.Collections.Generic;
using System.Text;

namespace WorkforceManagement.DAL.Concrete
{
    public class DbInitalizer
    {
        public static void Initalize(EFDbContext context)
        {
            context.Database.EnsureCreated();
            ContextAccessor.Context = context;
        }
    }
}
