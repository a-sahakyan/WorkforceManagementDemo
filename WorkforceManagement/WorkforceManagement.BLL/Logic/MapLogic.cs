using System.Collections.Generic;
using WorkforceManagement.DAL.DataProvider;

namespace WorkforceManagement.BLL.Logic
{
    public class MapLogic<TSource,TDestination> : IMapLogic<TSource,TDestination> where TSource : class where TDestination : class
    {
        private IRepository<TSource> _entity;
        
        public MapLogic(IRepository<TSource> entity)
        {
            _entity = entity;
        }

        public IEnumerable<TDestination> MapAll()
        {
            var model = AutoMapper.Mapper.Map<IEnumerable<TDestination>>(_entity.GetAll());

            return model;
        }

        public TSource Map(TDestination entity)
        {
            var model = AutoMapper.Mapper.Map<TSource>(entity);

            return model;
        }
    }
}
