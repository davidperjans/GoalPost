using System;
using System.Security.Claims;
using System.Threading.Tasks;
using GoalPost.Application.DTOs.Auth;
using GoalPost.Application.Features.Auth.Commands;
using GoalPost.Core.Entities;
using GoalPost.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GoalPost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;

        public AuthController(IMediator mediator, UserManager<User> userManager, IUserRepository userRepository)
        {
            _mediator = mediator;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var command = new RegisterCommand
                {
                    Username = registerDto.Username,
                    Email = registerDto.Email,
                    Password = registerDto.Password
                };

                var (success, token, error) = await _mediator.Send(command);
                if (!success)
                {
                    return BadRequest(new AuthResponse { Error = error });
                }

                var user = await _userRepository.GetByEmailAsync(registerDto.Email);
                if (user == null)
                {
                    return BadRequest(new AuthResponse { Error = "Kunde inte hitta den skapade användaren" });
                }

                var roles = await _userManager.GetRolesAsync(user);
                var userDto = new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    ProfileImage = user.ProfileImage,
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt,
                    LastLoginAt = user.LastLoginAt,
                    Roles = roles.ToList()
                };

                return Ok(new AuthResponse { Token = token, User = userDto });
            }
            catch (Exception ex)
            {
                return BadRequest(new AuthResponse { Error = "Ett oväntat fel uppstod vid registrering" });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var command = new LoginCommand
                {
                    Email = loginDto.Email,
                    Password = loginDto.Password
                };

                var (success, token, error) = await _mediator.Send(command);
                if (!success)
                {
                    return BadRequest(new AuthResponse { Error = error });
                }

                var user = await _userRepository.GetByEmailAsync(loginDto.Email);
                if (user == null)
                {
                    return BadRequest(new AuthResponse { Error = "Kunde inte hitta användaren" });
                }

                var roles = await _userManager.GetRolesAsync(user);
                var userDto = new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    ProfileImage = user.ProfileImage,
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt,
                    LastLoginAt = user.LastLoginAt,
                    Roles = roles.ToList()
                };

                return Ok(new AuthResponse { Token = token, User = userDto });
            }
            catch (Exception ex)
            {
                return BadRequest(new AuthResponse { Error = "Ett oväntat fel uppstod vid inloggning" });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            var (success, message) = await _mediator.Send(command);
            
            if (!success)
            {
                return BadRequest(new { message });
            }

            return Ok(new { message });
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));
                if (user == null)
                {
                    return NotFound();
                }

                var roles = await _userManager.GetRolesAsync(user);
                var userDto = new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    ProfileImage = user.ProfileImage,
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt,
                    LastLoginAt = user.LastLoginAt,
                    Roles = roles.ToList()
                };

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Ett oväntat fel uppstod vid hämtning av användarinformation" });
            }
        }
    }
} 