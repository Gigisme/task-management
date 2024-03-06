using Domain.Models;
using Domain.Models.DTO;
using Domain.Models.DTO.UserTask;
using Domain.Models.Enums;
using Domain.Services.IServices;

namespace Domain.Services;

public class AdapterService : IAdapterService
{
    public LoginResponseDto LoginResponse(string username, string token) => new()
    {
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

    public UserTask UserTaskFromDto(CreateDto userTask, int userId) => new()
    {
        Name = userTask.Name,
        Description = userTask.Description,
        Status = UserTaskStatus.Pending,
        UserId = userId,
    };

    public DisplayDto DtoFromUserTask(UserTask userTask) => new()
    {
        Id = userTask.Id,
        Name = userTask.Name,
        Description = userTask.Description,
        Status = userTask.Status,
    };
}