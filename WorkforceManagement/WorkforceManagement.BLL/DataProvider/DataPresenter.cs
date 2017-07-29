using WorkforceManagement.DAL.DataProvider;

namespace WorkforceManagement.BLL.DataProvider
{
    public class DataPresenter<TModel> : IDataPresenter<TModel> where TModel : class
    {
        IRepository<TModel> _data;

        public DataPresenter(IRepository<TModel> data)
        {
            _data = data;
        }

        IRepository<TModel> IDataPresenter<TModel>.Data
        {
            get { return _data; }
        }
    }
}
