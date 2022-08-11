namespace Common.DataAccess
{
    internal interface IUnitOfWork
    {
        void Save();

        IRepository<T, K> GetRepository<T, K>() where T : class;
    }
}