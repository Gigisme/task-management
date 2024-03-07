using System.ComponentModel.DataAnnotations;
using Domain.Models.Enums;

namespace Domain.Models.DTO.UserTask;

public class PatchRequestDto
{
    [Required(ErrorMessage = "Id is required")]
    public int? Id { get; set; }

    [Required(ErrorMessage = "Task status is required")]
    [EnumDataType(typeof(UserTaskStatus))]
    public UserTaskStatus? Status { get; set; }
}