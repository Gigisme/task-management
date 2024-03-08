using Domain.Models;
using Domain.Models.DTO.User;
using Domain.Models.DTO.UserTask;
using Domain.Models.Enums;
using Domain.Services;

namespace Tests.ServiceTests;

public class AdapterServiceTests
{
    private readonly AdapterService _adapterService = new();

    [Fact]
    public void LoginResponse_WhenCalled_ReturnsLoginResponseDto()
    {
        // Arrange
        var username = "username";
        var token = "testToken";
        
        // Act
        var result = _adapterService.LoginResponse(username, token);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(username, result.Username);
        Assert.Equal(token, result.Token);
    }

    [Fact]
    public void UserFromRegistration_WhenCalled_ReturnsUser()
    {
        // Arrange
        var registration = new RegistrationDto
        {
            Username = "username",
            Email = "email",
            Password = "password"
        };
        var hashedPassword = new Tuple<byte[], byte[]>(new byte[0], new byte[0]);
        
        // Act
        var result = _adapterService.UserFromRegistration(registration, hashedPassword);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(registration.Username, result.Username);
        Assert.Equal(registration.Email, result.Email);
        Assert.Equal(hashedPassword.Item1, result.PasswordHash);
        Assert.Equal(hashedPassword.Item2, result.PasswordSalt);
    }
    
    [Fact]
    public void UserTaskFromDto_WhenCalled_ReturnsUserTask()
    {
        // Arrange
        var userTask = new CreateDto
        {
            Name = "name",
            Description = "description"
        };
        var userId = 1;
        
        // Act
        var result = _adapterService.UserTaskFromDto(userTask, userId);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(userTask.Name, result.Name);
        Assert.Equal(userTask.Description, result.Description);
        Assert.Equal(userId, result.UserId);
    }
    
    [Fact]
    public void DtoFromUserTask_WhenCalled_ReturnsDisplayDto()
    {
        // Arrange
        var userTask = new UserTask
        {
            Id = 1,
            Name = "name",
            Description = "description",
            Status = UserTaskStatus.Pending
        };
        
        // Act
        var result = _adapterService.DtoFromUserTask(userTask);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(userTask.Id, result.Id);
        Assert.Equal(userTask.Name, result.Name);
        Assert.Equal(userTask.Description, result.Description);
        Assert.Equal(userTask.Status, result.Status);
    }
}