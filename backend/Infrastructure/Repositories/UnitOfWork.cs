using Infrastructure.Database;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    
    private readonly TaskDbContext _context;
    public IUserRepository UserRepository { get; private set; }
    public IUserTaskRepository UserTaskRepository { get; private set; }


    public UnitOfWork(
        TaskDbContext context,
        IUserRepository userRepository,
        IUserTaskRepository postRepository)
    {
        _context = context;
        UserRepository = userRepository;
        UserTaskRepository = postRepository;
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}