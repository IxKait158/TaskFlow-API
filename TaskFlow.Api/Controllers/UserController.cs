using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Services.Interfaces;

namespace TaskFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase {
    private readonly IUserService _userService;

    public UserController(IUserService userService) {
        _userService = userService;
    }

    [Authorize]
    [HttpGet("my-profile")]
    public async Task<IActionResult> GetMyProfile() {
        var userDto = await _userService.GetMyProfileAsync();
        
        return Ok(userDto);
    }
    
    [Authorize("AdminPolicy")]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id) {
        var userDto = await _userService.GetUserByIdAsync(id);

        return Ok(userDto);
    }
    
    [Authorize("AdminPolicy")]
    [HttpGet("all-users")]
    public async Task<IActionResult> GetAll() {
        var usersDto = await _userService.GetAllUsersAsync();

        return Ok(usersDto);
    }
}