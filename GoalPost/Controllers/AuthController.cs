using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using GoalPost.Application.DTOs.Auth;
using GoalPost.Application.Features.Auth.Commands.Login;
using GoalPost.Application.Features.Auth.Commands.RegisterUser;
using GoalPost.Application.Features.Auth.Commands.UpdateProfile;
using System.Security.Claims;

namespace GoalPost.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto registerDto)
    {
        var command = new RegisterUserCommand { RegisterDto = registerDto };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
    {
        var command = new LoginCommand { LoginDto = loginDto };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [Authorize]
    [HttpPut("profile")]
    public async Task<ActionResult<AuthResponseDto>> UpdateProfile(UpdateProfileDto updateProfileDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var command = new UpdateProfileCommand 
        { 
            UserId = userId,
            UpdateProfileDto = updateProfileDto
        };
        
        var result = await _mediator.Send(command);
        return Ok(result);
    }
} 