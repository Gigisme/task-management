using Infrastructure.Database;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
{
    protected readonly TaskDbContext Context;
    protected readonly DbSet<TEntity> Entities;
    
    public Repository(TaskDbContext context)
    {
        Context = context;
        Entities = context.Set<TEntity>();
    }
    
    public Task<TEntity?> GetByIdAsync(TKey id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TEntity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(TEntity entity)
    {
        await Entities.AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public Task UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }
}