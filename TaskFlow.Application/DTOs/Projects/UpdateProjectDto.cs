using TaskFlow.Application.DTOs.Tasks;

namespace TaskFlow.Application.DTOs.Projects;

public record UpdateProjectDto {
    public string? Name { get; init; } = null;
    public string? Description { get; init; } = null;
    
    public Guid? OwnerId { get; init; } = null;
}