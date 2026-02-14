using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs.Projects;
using TaskFlow.Application.Services.Interfaces;

namespace TaskFlow.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase {
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService) {
        _projectService = projectService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProjectDto request) {
        var createdProject = await _projectService.CreateProjectAsync(request);

        return CreatedAtAction(nameof(Get), new { id = createdProject.Id }, createdProject);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id) {
        var projectDto = await _projectService.GetProjectAsync(id);

        return Ok(projectDto);
    }
    
    [HttpGet("{id:guid}/tasks")]
    public async Task<IActionResult> GetTasks(Guid id) {
        var projectWithTaskDto = await _projectService.GetProjectTasksAsync(id);

        return Ok(projectWithTaskDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProjectDto updateDto) {
        var project = await _projectService.UpdateProjectAsync(id, updateDto);
        
        return Ok(project);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id) {
        await _projectService.DeleteProjectAsync(id);

        return NoContent();
    }
}