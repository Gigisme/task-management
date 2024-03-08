using System.IdentityModel.Tokens.Jwt;
using Domain.Services;
using Microsoft.Extensions.Configuration;

namespace Tests.ServiceTests;

public class JwtServiceTests
{
    private readonly JwtService _jwtService;
    public JwtServiceTests()
    {
        var conf = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _jwtService = new JwtService(conf);
    }
    
    [Fact]
    public void GenerateJwtToken_WhenCalled_ReturnsJwtToken()
    {
        // Arrange
        var userId = 1;

        // Act
        var result = _jwtService.GenerateJwtToken(userId);

        // Assert
        Assert.NotNull(result);
    }
    
    [Fact]
    public void GenerateJwtToken_WhenCalled_ReturnsJwtTokenWithClaims()
    {
        // Arrange
        var userId = 1;

        // Act
        var result = _jwtService.GenerateJwtToken(userId);
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(result);
        var uniqueNameClaim = token.Claims.FirstOrDefault(c => c.Type == "unique_name");
        
        // Assert
        Assert.NotNull(uniqueNameClaim);
        Assert.Equal(userId.ToString(), uniqueNameClaim.Value);
    }
}