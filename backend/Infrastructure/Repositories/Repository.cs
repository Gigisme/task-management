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
    
    public async Task<TEntity?> GetByIdAsync(TKey id)
    {
        var entity = await Entities.FindAsync(id);
        return entity;
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

    public async Task UpdateAsync(TEntity entity)
    {
        Entities.Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        Entities.Remove(entity);
        await Context.SaveChangesAsync();
    }
}