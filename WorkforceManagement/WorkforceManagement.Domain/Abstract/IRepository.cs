using System;
using System.Collections.Generic;
using System.Text;

namespace WorkforceManagement.Domain.Abstract
{
    public interface IRepository<TModel> where TModel : class
    {
        IEnumerable<TModel> Model { get; set; }
    }
}
