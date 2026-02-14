using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs.Authentication;
using TaskFlow.Application.Services.Interfaces;

namespace TaskFlow.Api.Controllers;

public class AuthController : ControllerBase {
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) {
        _authService =  authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest) {
        await _authService.Register(registerRequest);
        
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest) {
        var result = await _authService.Login(loginRequest);

        HttpContext.Response.Cookies.Append("secretCookies", result.Token);

        return Ok(new { user = result.User, token = result.Token });
    }

    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout() {
        HttpContext.Response.Cookies.Delete("secretCookies");

        return Ok(new { message = "You have successfully logged out." });
    }
}