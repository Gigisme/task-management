using Domain.Models;

namespace Infrastructure.Repositories.Interfaces;

public interface IUserTaskRepository : IRepository<UserTask, int>
{
    Task<IEnumerable<UserTask>> GetAllAsync(int userId);
}