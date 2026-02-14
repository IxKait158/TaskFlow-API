using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Common.Exception;
using TaskFlow.Application.Common.Extensions;
using TaskFlow.Application.Common.Interfaces;
using TaskFlow.Application.Common.Interfaces.Authentication;
using TaskFlow.Application.DTOs.Authentication;
using TaskFlow.Application.DTOs.User;
using TaskFlow.Application.Services.Interfaces;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Services.Implementations;

public class AuthService : IAuthService {
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IValidator<LoginRequest> _loginValidator;
    private readonly IValidator<RegisterRequest> _registerValidator;

    public AuthService(
        IPasswordHasher passwordHasher, 
        IJwtProvider jwtProvider,
        IMapper mapper,
        IUnitOfWork unitOfWork, 
        IValidator<LoginRequest> loginValidator,
        IValidator<RegisterRequest> registerValidator) 
    {
        _passwordHasher =  passwordHasher;
        _jwtProvider = jwtProvider;

        _mapper = mapper;
        
        _unitOfWork = unitOfWork;
        
        _loginValidator = loginValidator;
        _registerValidator = registerValidator;
    }
    
    public async Task Register(RegisterRequest registerRequest) {
        await _registerValidator.ValidateAndThrowCustomAsync(registerRequest);

        if (await _unitOfWork.UsersRepository.IsEmailExistsAsync(registerRequest.Email))
            throw new ConflictException("User with this email already exists.");
        
        var hashedPassword = _passwordHasher.Generate(registerRequest.Password);

        var user = new User
        {
            Username = registerRequest.Username,
            Email = registerRequest.Email,
            PasswordHash = hashedPassword
        };

        try {
            await _unitOfWork.UsersRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateException) {
            throw new ConflictException("User already exists (concurrency conflict).");
        }
    }
    
    public async Task<(string Token, UserDto User)> Login(LoginRequest loginRequest) {
        await _loginValidator.ValidateAndThrowCustomAsync(loginRequest);
        
        var user = await _unitOfWork.UsersRepository.GetByEmailAsync(loginRequest.Email);

        bool isPasswordValid = user != null && _passwordHasher.Verify(loginRequest.Password, user.PasswordHash);

        if (user == null || !isPasswordValid)
            throw new UnauthorizedAccessException("Invalid email or password");
        
        var token = _jwtProvider.GenerateToken(user);
        
        return (token, _mapper.Map<UserDto>(user));
    }
}