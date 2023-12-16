namespace Domain.Models;

public class UserTask
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public TaskStatus Status { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}