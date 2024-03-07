using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<TEntity, TKey>(DbContext context) : IRepository<TEntity, TKey>
    where TEntity : class
{
    protected readonly DbSet<TEntity> Entities = context.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(TKey id)
    {
        var entity = await Entities.FindAsync(id);
        return entity;
    }

    public async Task AddAsync(TEntity entity)
    {
        await Entities.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        Entities.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        Entities.Remove(entity);
        await context.SaveChangesAsync();
    }
}