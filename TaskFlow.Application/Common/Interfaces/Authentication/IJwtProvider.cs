using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Common.Interfaces.Authentication;

public interface IJwtProvider {
    string GenerateToken(User user);
}