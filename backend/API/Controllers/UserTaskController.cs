using Domain.Models.DTO.UserTask;
using Domain.Models.Enums;
using Domain.Services.IServices;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/task")]
[ApiController]
public class UserTaskController(
    IUnitOfWork unitOfWork,
    IHttpContextAccessor contextAccessor,
    IAdapterService adapterService) : Controller
{
    [HttpPost("create")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DisplayDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateTask([FromBody] CreateDto createDto)
    {
        var userIdString = contextAccessor.HttpContext?.User.Identity?.Name;
        if (!int.TryParse(userIdString, out var userId)) return BadRequest();

        var userTask = adapterService.UserTaskFromDto(createDto, userId);
        await unitOfWork.UserTaskRepository.AddAsync(userTask);

        var displayDto = adapterService.DtoFromUserTask(userTask);

        return Created($"/api/tasks?id={userTask.Id}", displayDto);
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DisplayDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromQuery] int id)
    {
        var userIdString = contextAccessor.HttpContext?.User.Identity?.Name;
        if (!int.TryParse(userIdString, out var userId)) return BadRequest();

        var userTask = await unitOfWork.UserTaskRepository.GetByIdAsync(id);
        if (userTask == null) return NotFound();

        if (userTask.UserId != userId) return Unauthorized();

        var dto = adapterService.DtoFromUserTask(userTask);
        return Ok(dto);
    }

    [HttpGet("all")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DisplayDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAllTasks()
    {
        var userIdString = contextAccessor.HttpContext?.User.Identity?.Name;
        if (!int.TryParse(userIdString, out var userId)) return BadRequest();

        var userTasks = await unitOfWork.UserTaskRepository.GetAllAsync(userId);
        List<DisplayDto> userTaskDtos = [];
        userTaskDtos.AddRange(userTasks.Select(adapterService.DtoFromUserTask));

        return Ok(userTaskDtos);
    }

    [HttpPatch("patch")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DisplayDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangeStatus([FromBody] PatchRequestDto patchDto)
    {
        var userIdstring = contextAccessor.HttpContext?.User.Identity?.Name;
        if (!int.TryParse(userIdstring, out var userId)) return BadRequest();

        var userTask = await unitOfWork.UserTaskRepository.GetByIdAsync((int)patchDto.Id!);
        if (userTask == null) return NotFound();

        if (userTask.UserId != userId) return Unauthorized();

        userTask.Status = (UserTaskStatus)patchDto.Status!;
        await unitOfWork.UserTaskRepository.UpdateAsync(userTask);

        var responseDto = adapterService.DtoFromUserTask(userTask);

        return Ok(responseDto);
    }

    [HttpPut("update")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DisplayDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Edit([FromBody] UpdateRequestDto updateDto)
    {
        var userIdString = contextAccessor.HttpContext?.User.Identity?.Name;
        if (!int.TryParse(userIdString, out var userId)) return BadRequest();

        var userTask = await unitOfWork.UserTaskRepository.GetByIdAsync((int)updateDto.Id!);
        if (userTask == null) return NotFound();

        if (userTask.UserId != userId) return Unauthorized();

        userTask.Name = updateDto.Name;
        userTask.Description = updateDto.Description;
        await unitOfWork.UserTaskRepository.UpdateAsync(userTask);

        var userTaskDto = adapterService.DtoFromUserTask(userTask);

        return Ok(userTaskDto);
    }

    [HttpDelete("delete")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var userIdString = contextAccessor.HttpContext?.User.Identity?.Name;
        if (!int.TryParse(userIdString, out var userId)) return BadRequest();

        var userTask = await unitOfWork.UserTaskRepository.GetByIdAsync(id);
        if (userTask == null) return NotFound();

        if (userTask.UserId != userId) return Unauthorized();

        await unitOfWork.UserTaskRepository.DeleteAsync(userTask);
        return NoContent();
    }
}