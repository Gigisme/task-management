using Domain.Models;
using Domain.Models.DTO.User;
using Domain.Models.DTO.UserTask;
using Domain.Models.Enums;
using Domain.Services.IServices;

namespace Domain.Services;

public class AdapterService : IAdapterService
{
    public LoginResponseDto LoginResponse(string username, string token)
    {
        return new LoginResponseDto
        {
            Username = username,
            Token = token
        };
    }

    public User UserFromRegistration(RegistrationDto registration, Tuple<byte[], byte[]> hashedPassword)
    {
        return new User
        {
            Username = registration.Username,
            Email = registration.Email,
            PasswordHash = hashedPassword.Item1,
            PasswordSalt = hashedPassword.Item2,
            CreatedAt = DateTime.UtcNow
        };
    }

    public UserTask UserTaskFromDto(CreateDto userTask, int userId)
    {
        return new UserTask
        {
            Name = userTask.Name,
            Description = userTask.Description,
            Status = UserTaskStatus.Pending,
            UserId = userId
        };
    }

    public DisplayDto DtoFromUserTask(UserTask userTask)
    {
        return new DisplayDto
        {
            Id = userTask.Id,
            Name = userTask.Name,
            Description = userTask.Description,
            Status = userTask.Status
        };
    }
}