using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.Services.Interfaces;

public interface ICurrentUserService {
    Guid UserId { get; }
    string Role { get; }
}