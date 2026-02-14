using FluentValidation;
using TaskFlow.Application.Common.Mapping;
using TaskFlow.Application.Services.Implementations;
using TaskFlow.Application.Services.Interfaces;

namespace TaskFlow.Application.Common.Extensions;

public static class ApplicationExtension {
    public static IServiceCollection AddApplication(this IServiceCollection services) {
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ITaskItemService, TaskItemService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        
        services.AddValidatorsFromAssembly(typeof(ApplicationExtension).Assembly);
        services.AddAutoMapper(typeof(ApplicationExtension));
        
        return services;
    }
}