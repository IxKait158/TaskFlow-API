namespace TaskFlow.Application.DTOs.Authentication;

public record LoginRequest(
    string Email,
    string Password);