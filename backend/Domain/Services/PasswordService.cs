using System.Security.Cryptography;
using System.Text;
using Domain.Services.IServices;

namespace Domain.Services;

public class PasswordService : IPasswordService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="password"></param>
    /// <returns>Hash, Salt</returns>
    public Tuple<byte[], byte[]> HashPassword(string password)
    {
        using var hmac = new HMACSHA512();
        var salt = hmac.Key;
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return new Tuple<byte[], byte[]>(hash, salt);
    }
    
    public bool VerifyPassword(string password, byte[] hash, byte[] salt )
    {
        using var hmac = new HMACSHA512(salt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(hash);
    }
}