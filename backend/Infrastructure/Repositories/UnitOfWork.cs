using Infrastructure.Database;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories;

public class UnitOfWork(
    TaskDbContext context,
    IUserRepository userRepository,
    IUserTaskRepository postRepository)
    : IUnitOfWork
{
    public IUserRepository UserRepository { get; } = userRepository;
    public IUserTaskRepository UserTaskRepository { get; } = postRepository;

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}