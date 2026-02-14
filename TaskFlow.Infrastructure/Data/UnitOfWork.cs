using TaskFlow.Application.Common.Interfaces;
using TaskFlow.Application.Common.Interfaces.Repositories;
using TaskFlow.Infrastructure.Repositories;

namespace TaskFlow.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork {
    private readonly AppDbContext _dbContext;
    
    public IUserRepository UsersRepository { get; }
    public ITaskItemRepository TaskItemsRepository { get; }
    public IProjectRepository ProjectsRepository { get; }

    public UnitOfWork(AppDbContext dbContext) {
        _dbContext = dbContext;
        
        UsersRepository = new UserRepository(dbContext);
        TaskItemsRepository = new TaskItemRepository(dbContext);
        ProjectsRepository = new ProjectRepository(dbContext);
    }

    public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}