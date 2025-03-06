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

        // Find user by email
        var user = await _userRepository.GetByEmailAsync(loginDto.Email);
        if (user == null)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Felaktig e-postadress eller lösenord"
            };
        }

        // Check if user is active
        if (!user.IsActive)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Kontot är inaktiverat"
            };
        }

        // Attempt to sign in
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Felaktig e-postadress eller lösenord"
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
            Message = "Inloggning lyckades",
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