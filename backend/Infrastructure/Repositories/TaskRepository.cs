using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories;

public class TaskRepository(TaskDbContext db) : Repository<UserTask, int>(db), ITaskRepository
{
    
}