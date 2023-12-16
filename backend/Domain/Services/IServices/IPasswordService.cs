namespace Domain.Services.IServices;

public interface IPasswordService
{
    Tuple<byte[], byte[]> HashPassword(string password);
    bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt);
}