using Domain.Models;
using Domain.Models.DTO;
using Domain.Models.DTO.UserTask;

namespace Domain.Services.IServices;

public interface IAdapterService
{
    LoginResponseDto LoginResponse(string username, string token);
    User UserFromRegistration(RegistrationDto registration, Tuple<byte[],byte[]> hashedPassword);
    UserTask UserTaskFromDto(CreateDto userTask, int userId);
    DisplayDto DtoFromUserTask(UserTask userTask);
}