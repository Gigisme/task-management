using Domain.Models.Enums;

namespace Domain.Models.DTO.UserTask;

public class DisplayDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public UserTaskStatus Status { get; set; }
}