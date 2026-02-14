using TaskFlow.Application.DTOs.User;

namespace TaskFlow.Application.DTOs.Projects;

public record ProjectDto {
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }

    public UserDto Owner { get; init; }
}