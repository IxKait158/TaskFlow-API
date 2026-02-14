namespace TaskFlow.Application.Common.Interfaces.Repositories;

public interface IBaseRepository<T> {
    Task<IReadOnlyList<T>> GetAllAsync();
    
    Task<IReadOnlyList<T>> GetByPageAsync(int page, int pageSize);
    
    Task<T?> GetByIdAsync(Guid id);
    
    Task AddAsync(T entity);
    
    Task DeleteAsync(T entity);
}