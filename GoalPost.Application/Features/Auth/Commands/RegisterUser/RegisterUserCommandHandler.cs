using MediatR;
using GoalPost.Core.Domain;
using GoalPost.Core.Interfaces;
using GoalPost.Application.DTOs.Auth;
using System.Text.RegularExpressions;
using GoalPost.Application.Services;
using Microsoft.Extensions.Logging;

namespace GoalPost.Application.Features.Auth.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly JwtService _jwtService;
    private readonly ILogger<RegisterUserCommandHandler> _logger;

    public RegisterUserCommandHandler(
        IUserRepository userRepository, 
        JwtService jwtService,
        ILogger<RegisterUserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _logger = logger;
    }

    public async Task<AuthResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var registerDto = request.RegisterDto;

            // Validate email format
            if (!IsValidEmail(registerDto.Email))
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Invalid email format"
                };
            }

            // Check if user exists
            if (await _userRepository.ExistsAsync(registerDto.Email, registerDto.UserName))
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "User with this email or username already exists"
                };
            }

            // Create new user
            var user = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            var result = await _userRepository.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }

            // Generate JWT token
            var token = _jwtService.GenerateJwtToken(user);
            _logger.LogInformation("Generated JWT token for user {UserName}", user.UserName);

            return new AuthResponseDto
            {
                Success = true,
                Message = "User registered successfully",
                Token = token,
                User = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsActive = user.IsActive
                }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while registering user");
            return new AuthResponseDto
            {
                Success = false,
                Message = "An error occurred while registering the user"
            };
        }
    }

    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            // Basic email format validation
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(email))
                return false;

            // Additional checks
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
} 