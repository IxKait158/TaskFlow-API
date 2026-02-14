using TaskFlow.Application.DTOs.User;

namespace TaskFlow.Application.Services.Interfaces;

public interface IUserService {
    Task<IReadOnlyList<UserDto>> GetAllUsersAsync();
    
    Task<UserDto> GetUserByIdAsync(Guid userId);
    
    Task<UserDto> GetMyProfileAsync();
}