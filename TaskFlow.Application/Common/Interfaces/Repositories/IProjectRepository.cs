using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Common.Interfaces.Repositories;

public interface IProjectRepository : IBaseRepository<Project> {
    Task<Project?> GetByIdWithDetailsAsync(Guid projectId);
}