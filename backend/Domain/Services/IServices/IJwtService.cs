namespace Domain.Services.IServices;

public interface IJwtService
{
    string GenerateJwtToken(int userId);
}