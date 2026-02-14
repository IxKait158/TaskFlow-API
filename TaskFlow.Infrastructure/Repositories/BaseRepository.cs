using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Common.Interfaces.Repositories;
using TaskFlow.Domain.Entities;
using TaskFlow.Infrastructure.Data;

namespace TaskFlow.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity {
    protected readonly DbSet<T> _dbSet;
    
    public BaseRepository(AppDbContext dbContext) {
        _dbSet = dbContext.Set<T>();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync() => 
        await _dbSet
            .AsNoTracking()
            .ToListAsync();
    
    public async Task<IReadOnlyList<T>> GetByPageAsync(int page, int pageSize) => 
        await _dbSet
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    
    public async Task<T?> GetByIdAsync(Guid id) => 
        await _dbSet
            .FirstOrDefaultAsync(p => p.Id == id);
    
    public async Task AddAsync(T entity) =>
        await _dbSet.AddAsync(entity);
    
    public async Task DeleteAsync(T entity) => 
        await _dbSet
            .Where(p => p.Id == entity.Id)
            .ExecuteDeleteAsync();
}