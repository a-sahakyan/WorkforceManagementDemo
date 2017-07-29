using System.Collections.Generic;

namespace WorkforceManagement.DAL.DataProvider
{
    public interface IRepository<TModel> where TModel : class
    {
        TModel GetById { get; }

        IEnumerable<TModel> GetAll();

        void Insert(TModel data);

        void Delete(TModel data);

        void Update(TModel data);
    }
}
