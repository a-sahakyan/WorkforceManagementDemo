using System.Collections.Generic;
using System.Threading.Tasks;
using WorkforceManagement.Domain.Entities;

namespace WorkforceManagement.DAL.DataProvider
{
    public interface IRepository<TModel> where TModel : class
    {
        TModel GetById { get; }

        IEnumerable<TModel> GetAll();

        void Insert(TModel data);

        void Delete(TModel data);

        void Update(Skill updated,Skill original);
    }
}
