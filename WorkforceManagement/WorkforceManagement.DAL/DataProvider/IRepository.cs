using System;
using System.Collections.Generic;
using System.Text;

namespace WorkforceManagement.DAL.DataProvider
{
    public interface IRepository<TModel> where TModel : class
    {
        TModel GetById { get; }

        IEnumerable<TModel> Get { get; }

        //getbyid, get, insert, delete, update

        void Insert(TModel data);

        void Delete(TModel data);

        void Update(TModel data);
    }
}
