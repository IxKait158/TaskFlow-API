using TaskFlow.Application.DTOs.Authentication;
using TaskFlow.Application.DTOs.User;

namespace TaskFlow.Application.Services.Interfaces;

public interface IAuthService {
    Task Register(RegisterRequest registerRequest);
    
    Task<(string Token, UserDto User)> Login(LoginRequest loginRequest);
}