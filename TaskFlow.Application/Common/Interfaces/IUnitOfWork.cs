using TaskFlow.Application.Common.Interfaces.Repositories;

namespace TaskFlow.Application.Common.Interfaces;

public interface IUnitOfWork {
    IUserRepository UsersRepository { get; }
    IProjectRepository ProjectsRepository { get; }
    ITaskItemRepository TaskItemsRepository { get; }
    
    Task<int> SaveChangesAsync();
}