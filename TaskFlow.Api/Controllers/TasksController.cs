using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs.Tasks;
using TaskFlow.Application.Services.Interfaces;

namespace TaskFlow.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TasksController : ControllerBase {
    private readonly ITaskItemService _taskItemService;

    public TasksController(ITaskItemService taskItemService) {
        _taskItemService = taskItemService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskItemDto createRequest) {
        var createdProject = await _taskItemService.CreateTaskItemAsync(createRequest);

        return CreatedAtAction(nameof(Create), new { id = createdProject.Id }, createdProject);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id) {
        var task = await _taskItemService.GetTaskItemAsync(id);

        return Ok(task);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Edit(Guid id, [FromBody] UpdateTaskItemDto updateRequest) {
        var task = await _taskItemService.UpdateTaskItemAsync(id, updateRequest);

        return Ok(task);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id) {
        await  _taskItemService.DeleteTaskItemAsync(id);

        return NoContent();
    }
}