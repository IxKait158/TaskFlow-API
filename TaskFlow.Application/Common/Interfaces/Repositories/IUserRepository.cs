using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Common.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User> {
    Task<User?> GetByEmailAsync(string email);
    
    Task<bool> IsEmailExistsAsync(string email);
    
    Task<bool> IsUsernameExistsAsync(string username);
}