using AutoMapper;
using TaskFlow.Application.Common.Exception;
using TaskFlow.Application.Common.Interfaces;
using TaskFlow.Application.DTOs.User;
using TaskFlow.Application.Services.Interfaces;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Services.Implementations;

public class UserService : IUserService {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IMapper mapper) {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }
    
    public async Task<IReadOnlyList<UserDto>> GetAllUsersAsync() {
        var users = await _unitOfWork.UsersRepository.GetAllAsync();
        
        return _mapper.Map<IReadOnlyList<UserDto>>(users);
    }
    
    public async Task<UserDto> GetUserByIdAsync(Guid userId) {
        var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);
        
        if (user == null)
            throw new NotFoundException(nameof(User), userId);
        
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetMyProfileAsync() {
        var user = await _unitOfWork.UsersRepository.GetByIdAsync(_currentUserService.UserId);
        
        return _mapper.Map<UserDto>(user);
    }
}