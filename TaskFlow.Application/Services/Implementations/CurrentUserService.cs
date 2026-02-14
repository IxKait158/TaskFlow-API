using System.Security.Claims;
using TaskFlow.Application.Services.Interfaces;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.Services.Implementations;

public class CurrentUserService : ICurrentUserService {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor  httpContextAccessor) {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId {
        get {
            var idClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);

            if (idClaim == null)
                throw new UnauthorizedAccessException();
            
            return Guid.Parse(idClaim.Value);
        }
    }

    public string Role {
        get {
            var roleClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role);
            
            if (roleClaim == null)
                throw new UnauthorizedAccessException();

            return roleClaim.Value;
        }
    }
}