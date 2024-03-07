namespace Infrastructure.Repositories.Interfaces;

public interface IRepository<TEntity, TKey> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(TKey id);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}