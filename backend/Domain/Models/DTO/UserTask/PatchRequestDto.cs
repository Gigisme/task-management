using Domain.Models.Enums;

namespace Domain.Models.DTO.UserTask;

public class PatchRequestDto
{
    public int Id { get; set; }
    public UserTaskStatus Status { get; set; }
}