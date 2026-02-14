using TaskFlow.Application.DTOs.Tasks;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Common.Interfaces.Repositories;

public interface ITaskItemRepository : IBaseRepository<TaskItem> {
    Task<TaskItem?> GetTaskItemWithDetailsAsync(Guid taskId);

    Task<IReadOnlyList<TaskItem>> GetTaskItemsInProjectAsync(Guid projectId);
}