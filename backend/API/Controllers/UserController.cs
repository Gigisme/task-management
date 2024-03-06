using Domain.Models.DTO;
using Domain.Services.IServices;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api")]
[ApiController]
public class UserController(IPasswordService passwordService, IUnitOfWork unitOfWork, IJwtService jwtService,
    IAdapterService adapterService, IHttpContextAccessor contextAccessor) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromBody] RegistrationDto registration)
    {
        if (await unitOfWork.UserRepository.IsUsernameTakenAsync(registration.Username))
        {
            return Conflict("Username taken");
        }

        if (await unitOfWork.UserRepository.IsEmailTakenAsync(registration.Email))
        {
            return Conflict("Email taken");
        }
        
        var hashedPassword = passwordService.HashPassword(registration.Password);
        var user = adapterService.UserFromRegistration(registration, hashedPassword);
        
        await unitOfWork.UserRepository.AddAsync(user);
        user = await unitOfWork.UserRepository.GetByUsername(user.Username);

        string token = jwtService.GenerateJwtToken(user.Id);
        var response = adapterService.LoginResponse(user.Username, token);
        
        return Ok(response);
    }
    
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto login)
    {
        var user = await unitOfWork.UserRepository.GetByUsername(login.Username);
        if (user == null)
        {
            return Unauthorized("Invalid username");
        }

        var passwordMatch = passwordService.VerifyPassword(login.Password, user.PasswordHash, user.PasswordSalt);
        if (!passwordMatch)
        {
            return Unauthorized("Invalid password");
        }

        string token = jwtService.GenerateJwtToken(user.Id);
        var loginResponse = adapterService.LoginResponse(user.Username, token);
        
        return Ok(loginResponse);
    }

    [HttpGet("user")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetUser()
    {
        var userIdString = contextAccessor.HttpContext?.User.Identity?.Name;
        if (!int.TryParse(userIdString, out int userId)) { return BadRequest(); }

        var user = await unitOfWork.UserRepository.GetByIdAsync(userId);
        string token = jwtService.GenerateJwtToken(user.Id);
        var loginResponse = adapterService.LoginResponse(user.Username, token);

        return Ok(loginResponse);
    }
}