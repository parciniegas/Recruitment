using Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess
{
    public class Repository<T, K> : IRepository<T, K> where T : class
    {
        #region Private fields

        private readonly DbSet<T> _entitySet;
        private readonly DbContext _context;

        #endregion Private fields

        #region Constructors

        public Repository(DbContext context)
        {
            _context = context;
            _entitySet = _context.Set<T>();
        }

        #endregion Constructors

        #region IRepository implementation

        public IEnumerable<T> GetAll()
        {
            var list = _entitySet.ToList();

            return list;
        }

        public T GetById(K id)
        {
            var entity = _entitySet.Find(id);
            if (entity == null)
                throw new EntityNotFoundException($"Entity with id <{id}> not found");

            return entity;
        }

        public void Add(T entity)
        {
            _entitySet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(K id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        #endregion IRepository implementation
    }
}