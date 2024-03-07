using System.ComponentModel.DataAnnotations;

namespace Domain.Models.DTO.UserTask;

public class CreateDto
{
    [Required(ErrorMessage = "Title is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }
}