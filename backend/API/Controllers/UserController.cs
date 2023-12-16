using Domain.Models;
using Domain.Models.DTO;
using Domain.Services.IServices;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api")]
[ApiController]
public class UserController(IPasswordService passwordService, IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registration)
    {
        var hashedPassword = passwordService.HashPassword(registration.Password);
        var user = new User
        {
            Username = registration.Username,
            Email = registration.Email,
            PasswordHash = hashedPassword.Item1,
            PasswordSalt = hashedPassword.Item2,
            CreatedAt = DateTime.UtcNow,
        };

        try
        {
            await unitOfWork.UserRepository.AddAsync(user);
        }
        catch (Exception e)
        {
            return StatusCode(503, "Service Unavailable - Database Connection Error");
        }
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO login)
    {
        var user = await unitOfWork.UserRepository.GetByUsername(login.Username);
        if (user == null)
        {
            return Unauthorized();
        }

        var passwordMatch = passwordService.VerifyPassword(login.Password, user.PasswordHash, user.PasswordSalt);
        if (!passwordMatch)
        {
            return Unauthorized();
        }
        
        return Ok();
    }
}