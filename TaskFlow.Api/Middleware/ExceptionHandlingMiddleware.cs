using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Common.Exception;

namespace TaskFlow.Api.Middleware;

public class ExceptionHandlingMiddleware {
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger) {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context) {
        try {
            await _next(context);
        }
        catch (Exception ex) {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex) {
        _logger.LogError(ex,"Exception occurred: {Message}", ex.Message);

        context.Response.ContentType = "application/json";

        var (statusCode, message) = ex switch
        {
            NotFoundException => (404, ex.Message),
            ValidationException => (400, ex.Message),
            ForbiddenException => (403, "Access denied"),
            ConflictException => (409, ex.Message),
            UnauthorizedAccessException => (401, "Please login"),
            JsonException => (400, "Invalid JSON format"),
            BadHttpRequestException => (400, "Bad request body"),
            ArgumentNullException => (400, "Missing required arguments"),

            _ => (500, "Internal Server Error")
        };
        
        context.Response.StatusCode = statusCode;

        var response = new
        {
            status = statusCode,
            error = message
        };
        
        await context.Response.WriteAsJsonAsync(response);
    }
}