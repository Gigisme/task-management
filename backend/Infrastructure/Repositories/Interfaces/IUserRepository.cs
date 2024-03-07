using Domain.Models;

namespace Infrastructure.Repositories.Interfaces;

public interface IUserRepository : IRepository<User, int>
{
    Task<bool> IsEmailTakenAsync(string email);
    Task<bool> IsUsernameTakenAsync(string username);
    Task<User?> GetByUsername(string username);
}