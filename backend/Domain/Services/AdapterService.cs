using Domain.Models;
using Domain.Models.DTO;
using Domain.Services.IServices;

namespace Domain.Services;

public class AdapterService : IAdapterService
{
    public LoginResponseDto LoginResponse(string username, string token) => new()
    {
        Username = username,
        Token = token,
    };

    public User UserFromRegistration(RegistrationDto registration, Tuple<byte[],byte[]> hashedPassword) => new()
    {
        Username = registration.Username,
        Email = registration.Email,
        PasswordHash = hashedPassword.Item1,
        PasswordSalt = hashedPassword.Item2,
        CreatedAt = DateTime.UtcNow,
    };
}