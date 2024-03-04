using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(TaskDbContext db) : Repository<User, int>(db), IUserRepository
{
    public async Task<bool> IsEmailTakenAsync(string email)
    {
        return await Entities.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> IsUsernameTakenAsync(string username)
    {
        return await Entities.AnyAsync(u => u.Username == username);
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