using MediatR;
using Microsoft.AspNetCore.Identity;
using GoalPost.Core.Domain;
using GoalPost.Core.Interfaces;
using GoalPost.Application.DTOs.Auth;
using GoalPost.Application.Services;

namespace GoalPost.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly SignInManager<User> _signInManager;
    private readonly JwtService _jwtService;

    public LoginCommandHandler(
        IUserRepository userRepository, 
        SignInManager<User> signInManager,
        JwtService jwtService)
    {
        _userRepository = userRepository;
        _signInManager = signInManager;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var loginDto = request.LoginDto;

        // Find user by username
        var user = await _userRepository.GetByUserNameAsync(loginDto.UserName);
        if (user == null)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Invalid username or password"
            };
        }

        // Check if user is active
        if (!user.IsActive)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Account is deactivated"
            };
        }

        // Attempt to sign in
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Invalid username or password"
            };
        }

        // Update last login
        user.LastLoginAt = DateTime.UtcNow;
        await _userRepository.UpdateAsync(user);

        // Generate JWT token
        var token = _jwtService.GenerateJwtToken(user);

        return new AuthResponseDto
        {
            Success = true,
            Message = "Login successful",
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
} 