using System.ComponentModel.DataAnnotations;

namespace Domain.Models.DTO;

public class LoginRequestDto
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(20, ErrorMessage = "Password must be between 7 and 20 characters", MinimumLength = 7)]
    public string Password { get; set; }
}