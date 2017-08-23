using System.Linq;
using WorkforceManagement.Domain.Entities;

namespace WorkforceManagement.DAL.DataProvider
{
    public interface IRepository<TModel> where TModel : class
    {
        TModel GetById { get; }

        IQueryable<TModel> GetAll();

        void Insert(TModel data);

        void Delete(TModel data);

        void Update(Skill updated,Skill original);
    }
}
