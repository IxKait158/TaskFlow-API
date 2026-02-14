using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Enums;
using TaskFlow.Infrastructure.Authentication;

namespace TaskFlow.Api.Extensions;

public static class ApiExtensions {
    
    public static void AddApiAuthentication(this IServiceCollection services, IConfiguration configuration) {
        
        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
        
        if (jwtOptions == null) 
            throw new Exception("JwtOptions not found in appsettings configuration");
        
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                    
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["secretCookies"];
                            return Task.CompletedTask;
                        },
                        
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";

                            var response = new
                            {
                                status = 401,
                                error = "You are not logged in. Please log in.",
                                detail = context.ErrorDescription
                            };

                            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
                        },
                        
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            context.Response.ContentType = "application/json";

                            var response = new
                            {
                                status = 403,
                                error = "You do not have sufficient permissions to perform this operation."
                            };
                            
                            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
                        }
                    };
                });
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, nameof(EUserRole.Admin));
            });
            
            options.AddPolicy("UserPolicy", policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, nameof(EUserRole.User));
            });
        });
    }
}