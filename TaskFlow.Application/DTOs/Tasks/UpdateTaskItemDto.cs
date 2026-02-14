using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.DTOs.Tasks;

public record UpdateTaskItemDto {
    public string? Title { get; init; } = null;

    public string? Description { get; init; } = null;

    public ETaskStatus? Status {  get; init; } = null;

    public ETaskPriority? Priority { get; init; } = null;

    public DateTime? Deadline { get; init; } = null;

    public Guid? AssigneeId {get; init; } = null;
}