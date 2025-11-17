using System.Linq.Expressions;

namespace Project.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {

        Task<IEnumerable<T>> GetAllAsync(
       Expression<Func<T, bool>>? predicate = null,
       Func<IQueryable<T>, IQueryable<T>>? include = null
   );
        Task<T?> GetByIdAsync(object id, Func<IQueryable<T>, IQueryable<T>>? include = null);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);

        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);

        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

        Task<int> SaveChangesAsync();
    }
}
