namespace Infrastructure.Repositories.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    ITaskRepository TaskRepository { get; }
    Task SaveChangesAsync();
}