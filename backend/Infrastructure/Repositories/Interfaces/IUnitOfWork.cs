namespace Infrastructure.Repositories.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IUserTaskRepository UserTaskRepository { get; }
    Task SaveChangesAsync();
}