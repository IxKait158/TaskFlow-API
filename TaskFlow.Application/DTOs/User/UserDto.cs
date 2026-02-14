using System.Text.Json.Serialization;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.DTOs.User;

public record UserDto {
    public string Username { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public EUserRole Role { get; init; }
}