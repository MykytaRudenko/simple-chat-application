using System.Linq.Expressions;

namespace Data.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    
    Task<TEntity> GetByIdAsync(Guid id);
    
    Task AddAsync(TEntity entity);
    
    Task RemoveAsync(TEntity entity);
    
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
}