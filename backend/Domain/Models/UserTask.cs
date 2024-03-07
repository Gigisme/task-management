using Domain.Models.Enums;

namespace Domain.Models;

public class UserTask
{
    public int Id { get; init; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime CreatedAt { get; init; }
    public DateTime? CompletedAt { get; set; }
    public UserTaskStatus Status { get; set; }

    public int UserId { get; init; }
    public User User { get; set; } = null!;
}