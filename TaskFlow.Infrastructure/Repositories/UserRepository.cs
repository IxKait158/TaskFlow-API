using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Common.Interfaces.Repositories;
using TaskFlow.Domain.Entities;
using TaskFlow.Infrastructure.Data;

namespace TaskFlow.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository {
    public UserRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task<User?> GetByEmailAsync(string email) =>
        await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

    public async Task<bool> IsEmailExistsAsync(string email) => 
        await _dbSet.AnyAsync(u => u.Email.ToLower() == email.ToLower());

    public async Task<bool> IsUsernameExistsAsync(string username) =>
        await _dbSet.AnyAsync(u => u.Username.ToLower() == username.ToLower());
}