using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.DTOs.Tasks;

public record CreateTaskItemDto(
    string Title,
    string Description,
    ETaskPriority? Priority,
    DateTime? Deadline,
    Guid ProjectId,
    Guid? AssigneeId);