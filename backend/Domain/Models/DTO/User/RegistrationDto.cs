using System.ComponentModel.DataAnnotations;

namespace Domain.Models.DTO.User;

public class RegistrationDto
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(20, ErrorMessage = "Username must be between 3 and 20 characters", MinimumLength = 3)]
    public string Username { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(20, ErrorMessage = "Password must be between 7 and 20 characters", MinimumLength = 7)]
    public string Password { get; set; }
}