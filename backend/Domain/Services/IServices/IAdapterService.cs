using Domain.Models;
using Domain.Models.DTO;

namespace Domain.Services.IServices;

public interface IAdapterService
{
    LoginResponseDto LoginResponse(string username, string token);
    User UserFromRegistration(RegistrationDto registration, Tuple<byte[],byte[]> hashedPassword);
}