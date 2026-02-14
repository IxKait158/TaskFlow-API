namespace TaskFlow.Application.DTOs.Authentication;

public record RegisterRequest(
    string Username,
    string Email,
    string Password);
