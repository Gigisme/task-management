using Domain.Services;
using Xunit;

namespace Tests.ServiceTests
{
    public class PasswordServiceTests
    {
        private readonly PasswordService _passwordService = new();
        private const string Password = "password";

        [Fact]
        public void HashPassword_WithValidPassword_ReturnsHashAndSalt()
        {
            // Arrange
            var password = Password;
            
            // Act
            var (hash, salt) = _passwordService.HashPassword(password);
            
            // Assert
            Assert.NotNull(hash);
            Assert.NotNull(salt);
        }
        
        [Fact]
        public void VerifyPassword_WithValidPassword_ReturnsTrue()
        {
            // Arrange
            var password = Password;
            var (hash, salt) = _passwordService.HashPassword(password);
            
            // Act
            var result = _passwordService.VerifyPassword(password, hash, salt);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void VerifyPassword_WithInvalidPassword_ReturnsFalse()
        {
            // Arrange
            var password = Password;
            var (hash, salt) = _passwordService.HashPassword(password);
            
            // Act
            var result = _passwordService.VerifyPassword("invalidPassword", hash, salt);
            
            // Assert
            Assert.False(result);
        }
    }
}