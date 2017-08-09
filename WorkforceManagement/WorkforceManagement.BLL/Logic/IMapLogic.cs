using System.Collections.Generic;

namespace WorkforceManagement.BLL.Logic
{
    public interface IMapLogic<TSource, TDestination> where TSource : class where TDestination : class
    {
        IEnumerable<TDestination> MapAll();

        TSource Map(TDestination entity);
    }
}
