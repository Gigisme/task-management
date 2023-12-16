using System.ComponentModel.DataAnnotations;

namespace Domain.Models.DTO;

public class LoginRequestDTO
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}