namespace Common.DataAccess
{
    public interface IRepository<T, K> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(K id);

        void Add(T entity);

        void Update(T entity);

        void Delete(K id);

        void Save();
    }
}