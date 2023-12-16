using Domain.Models;
using Infrastructure.Database;

namespace Infrastructure.Repositories.Interfaces;

public interface IUserRepository : IRepository<User, int>
{
    Task<bool> IsRegisteredAsync(string email);
    Task<User?> GetByUsername(string username);
}