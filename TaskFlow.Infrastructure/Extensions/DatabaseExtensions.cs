using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Common.Interfaces;
using TaskFlow.Application.Common.Interfaces.Authentication;
using TaskFlow.Application.Common.Interfaces.Repositories;
using TaskFlow.Infrastructure.Authentication;
using TaskFlow.Infrastructure.Data;
using TaskFlow.Infrastructure.Repositories;

namespace TaskFlow.Infrastructure.Extensions;

public static class DatabaseExtensions {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ITaskItemRepository, TaskItemRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddSingleton<IJwtProvider, JwtProvider>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        
        return services;
    }
    
    public static void ApplyMigrations(this IApplicationBuilder app) {
        using var scope = app.ApplicationServices.CreateScope();
        
        using var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
        
        dbContext?.Database.Migrate();
    }
}