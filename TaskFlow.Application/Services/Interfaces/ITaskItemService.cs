using TaskFlow.Application.DTOs.Tasks;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Services.Interfaces;

public interface ITaskItemService {
    Task<TaskItemDto> CreateTaskItemAsync(CreateTaskItemDto createRequest);
    
    Task<TaskItemDto> GetTaskItemAsync(Guid taskId);
    
    Task<TaskItemDto> UpdateTaskItemAsync(Guid taskId, UpdateTaskItemDto updateRequest);
    
    Task DeleteTaskItemAsync(Guid taskId);
}