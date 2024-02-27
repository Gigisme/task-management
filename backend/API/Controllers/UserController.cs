using Domain.Models;
using Domain.Models.DTO;
using Domain.Services.IServices;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api")]
[ApiController]
public class UserController(IPasswordService passwordService, IUnitOfWork unitOfWork, IJwtService jwtService, IAdapterService adapterService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationDto registration)
    {
        var hashedPassword = passwordService.HashPassword(registration.Password);
        var user = adapterService.UserFromRegistration(registration, hashedPassword);

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
    public async Task<IActionResult> Login([FromBody] LoginRequestDto login)
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

        string token = jwtService.GenerateJwtToken(user.Id);
        var loginResponse = adapterService.LoginResponse(user.Username, token);
        
        return Ok(loginResponse);
    }
}