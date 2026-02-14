using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Common.Interfaces.Repositories;
using TaskFlow.Application.DTOs.Tasks;
using TaskFlow.Domain.Entities;
using TaskFlow.Infrastructure.Data;

namespace TaskFlow.Infrastructure.Repositories;

public class TaskItemRepository : BaseRepository<TaskItem>, ITaskItemRepository {
    public TaskItemRepository(AppDbContext dbContext) : base(dbContext) { }
    
    public async Task<TaskItem?> GetTaskItemWithDetailsAsync(Guid taskId) =>
        await _dbSet
            .Include(t => t.Project)
            .Include(t => t.Assignee)
            .FirstOrDefaultAsync(t => t.Id == taskId);

    public async Task<IReadOnlyList<TaskItem>> GetTaskItemsInProjectAsync(Guid projectId) =>
        await _dbSet
            .Include(t => t.Assignee)
            .Where(t => t.ProjectId == projectId)
            .ToListAsync();
}