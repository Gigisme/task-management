using Domain.Models.DTO.UserTask;
using Domain.Services.IServices;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/task")]
[ApiController]
public class UserTaskController(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor, IAdapterService adapterService) : Controller
{
    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> CreateTask([FromBody] CreateDto createDto)
    {
        var userIdString = contextAccessor.HttpContext?.User.Identity?.Name;
        if (!int.TryParse(userIdString, out int userId)) { return BadRequest(); }

        var userTask = adapterService.UserTaskFromDto(createDto, userId);
        await unitOfWork.UserTaskRepository.AddAsync(userTask);

        var displayDto = adapterService.DtoFromUserTask(userTask);

        return Created($"/api/tasks?id={userTask.Id}", displayDto);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get([FromQuery] int id)
    {
        var userIdstring = contextAccessor.HttpContext?.User.Identity?.Name;
        if (!int.TryParse(userIdstring, out int userId)) { return BadRequest(); }
        
        var userTask = await unitOfWork.UserTaskRepository.GetByIdAsync(id);
        if (userTask == null) return BadRequest();

        var dto = adapterService.DtoFromUserTask(userTask);
        return Ok(dto);
    }

    [HttpGet("all")]
    [Authorize]
    public async Task<IActionResult> GetAllTasks()
    {
        var userIdString = contextAccessor.HttpContext?.User.Identity?.Name;
        if (!int.TryParse(userIdString, out int userId)) { return BadRequest(); }

        var userTasks = await unitOfWork.UserTaskRepository.GetAllAsync(userId);
        List<DisplayDto> userTaskDtos = [];
        userTaskDtos.AddRange(userTasks.Select(adapterService.DtoFromUserTask));
        
        return Ok(userTaskDtos);
    }

    [HttpPatch("patch")]
    [Authorize]
    public async Task<IActionResult> ChangeStatus([FromBody] PatchRequestDto patchDto)
    {
        var userIdstring = contextAccessor.HttpContext?.User.Identity?.Name;
        if (!int.TryParse(userIdstring, out int userId)) { return BadRequest(); }
        
        var userTask = await unitOfWork.UserTaskRepository.GetByIdAsync(patchDto.Id);
        if (userTask == null) return BadRequest();

        if (userTask.UserId != userId) return Unauthorized();

        userTask.Status = patchDto.Status;
        await unitOfWork.UserTaskRepository.UpdateAsync(userTask);

        var responseDto = adapterService.DtoFromUserTask(userTask);
        
        return Ok(responseDto);
    }

    [HttpPost("update")]
    [Authorize]
    public async Task<IActionResult> Edit([FromBody] UpdateRequestDto updateDto)
    {
        var userIdString = contextAccessor.HttpContext?.User.Identity?.Name;
        if (!int.TryParse(userIdString, out int userId)) { return BadRequest(); }
        
        var userTask = await unitOfWork.UserTaskRepository.GetByIdAsync(updateDto.Id);
        if (userTask == null) return BadRequest();
        
        if (userTask.UserId != userId) return Unauthorized();

        userTask.Name = updateDto.Name;
        userTask.Description = updateDto.Description;
        await unitOfWork.UserTaskRepository.UpdateAsync(userTask);

        var userTaskDto = adapterService.DtoFromUserTask(userTask);
        
        return Ok(userTaskDto);
    }

    [HttpDelete("delete")]
    [Authorize]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var userIdString = contextAccessor.HttpContext?.User.Identity?.Name;
        if (!int.TryParse(userIdString, out int userId)) { return BadRequest(); }
        
        var userTask = await unitOfWork.UserTaskRepository.GetByIdAsync(id);
        if (userTask == null) return BadRequest();
        
        if (userTask.UserId != userId) return Unauthorized();

        await unitOfWork.UserTaskRepository.DeleteAsync(userTask);
        return NoContent();
    }
}