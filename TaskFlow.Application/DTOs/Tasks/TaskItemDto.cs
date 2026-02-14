using System.Text.Json.Serialization;
using TaskFlow.Application.DTOs.Projects;
using TaskFlow.Application.DTOs.User;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.DTOs.Tasks;

public record TaskItemDto {
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public ETaskStatus Status { get; init; }
    public ETaskPriority? Priority { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? Deadline { get; init; }

    public UserDto? Assignee { get; init; }
}