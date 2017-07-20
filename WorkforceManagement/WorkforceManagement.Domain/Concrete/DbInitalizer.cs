using System;
using System.Collections.Generic;
using System.Text;
using WorkforceManagement.Domain.Abstract;

namespace WorkforceManagement.Domain.Concrete
{
    public class DbInitalizer
    {
        public static void Initalize(EFDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
