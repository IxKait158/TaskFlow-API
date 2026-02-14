using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Common.Interfaces.Repositories;
using TaskFlow.Domain.Entities;
using TaskFlow.Infrastructure.Data;

namespace TaskFlow.Infrastructure.Repositories;

public class ProjectRepository : BaseRepository<Project>, IProjectRepository {
    public ProjectRepository(AppDbContext dbContext) : base(dbContext) { }
    
    public async Task<Project?> GetByIdWithDetailsAsync(Guid projectId) => 
        await _dbSet
            .Include(p => p.Owner)
            .Include(p => p.TaskItems)
            .FirstOrDefaultAsync(p => p.Id == projectId);
}