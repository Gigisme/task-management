using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserTaskRepository(TaskDbContext db) : Repository<UserTask, int>(db), IUserTaskRepository
{
    public async Task<IEnumerable<UserTask>> GetAllAsync(int userId)
    {
        var userTasks = await db.UserTasks.Where(ut => ut.UserId == userId).ToListAsync();
        return userTasks;
    }
}