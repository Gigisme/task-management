using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(TaskDbContext db) : Repository<User, int>(db), IUserRepository
{
    public Task<bool> IsRegisteredAsync(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByUsername(string username)
    {
        var user = await Entities.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null)
        {
            return null;
        }
        
        return user;
    }
}