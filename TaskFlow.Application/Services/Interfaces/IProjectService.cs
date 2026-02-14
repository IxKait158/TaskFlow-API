using TaskFlow.Application.DTOs.Projects;
using TaskFlow.Application.DTOs.Tasks;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Services.Interfaces;

public interface IProjectService {
    Task<ProjectDetailsDto> CreateProjectAsync(CreateProjectDto createRequest);
    
    Task<ProjectDto> GetProjectAsync(Guid projectId);
    
    Task<IReadOnlyList<TaskItemDto>> GetProjectTasksAsync(Guid projectId);
    
    Task<ProjectDto> UpdateProjectAsync(Guid projectId, UpdateProjectDto updateRequest);
    
    Task DeleteProjectAsync(Guid projectId);
}