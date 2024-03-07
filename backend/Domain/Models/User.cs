namespace Domain.Models;

public class User
{
    public int Id { get; init; }
    public string Username { get; init; } = null!;
    public string Email { get; init; } = null!;
    public byte[] PasswordHash { get; init; } = null!;
    public byte[] PasswordSalt { get; init; } = null!;
    public DateTime CreatedAt { get; init; }

    public ICollection<UserTask> UserTasks { get; set; } = null!;
}