using MediatR;
using Microsoft.AspNetCore.Identity;
using GoalPost.Core.Domain;
using GoalPost.Core.Interfaces;
using GoalPost.Application.DTOs.Auth;
using GoalPost.Application.Services;
using Microsoft.Extensions.Logging;

namespace GoalPost.Application.Features.Auth.Commands.UpdateProfile;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, AuthResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly JwtService _jwtService;
    private readonly ILogger<UpdateProfileCommandHandler> _logger;

    public UpdateProfileCommandHandler(
        IUserRepository userRepository,
        UserManager<User> userManager,
        JwtService jwtService,
        ILogger<UpdateProfileCommandHandler> logger)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _jwtService = jwtService;
        _logger = logger;
    }

    public async Task<AuthResponseDto> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            var updateDto = request.UpdateProfileDto;

            // Update username if provided
            if (!string.IsNullOrEmpty(updateDto.UserName) && updateDto.UserName != user.UserName)
            {
                var existingUser = await _userRepository.GetByUserNameAsync(updateDto.UserName);
                if (existingUser != null)
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Username is already taken"
                    };
                }
                user.UserName = updateDto.UserName;
            }

            // Update bio if provided
            if (updateDto.Bio != null)
            {
                user.Bio = updateDto.Bio;
            }

            // Update profile image URL if provided
            if (updateDto.ProfileImageUrl != null)
            {
                user.ProfileImageUrl = updateDto.ProfileImageUrl;
            }

            // Update password if provided
            if (!string.IsNullOrEmpty(updateDto.CurrentPassword) && 
                !string.IsNullOrEmpty(updateDto.NewPassword) && 
                !string.IsNullOrEmpty(updateDto.ConfirmNewPassword))
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, updateDto.CurrentPassword);
                if (!passwordCheck)
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Current password is incorrect"
                    };
                }

                var result = await _userManager.ChangePasswordAsync(user, updateDto.CurrentPassword, updateDto.NewPassword);
                if (!result.Succeeded)
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = string.Join(", ", result.Errors.Select(e => e.Description))
                    };
                }
            }

            // Save changes
            var updateResult = await _userRepository.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = string.Join(", ", updateResult.Errors.Select(e => e.Description))
                };
            }

            // Generate new token
            var token = _jwtService.GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Success = true,
                Message = "Profile updated successfully",
                Token = token,
                User = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsActive = user.IsActive,
                    Bio = user.Bio,
                    ProfileImageUrl = user.ProfileImageUrl
                }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating profile");
            return new AuthResponseDto
            {
                Success = false,
                Message = "An error occurred while updating the profile"
            };
        }
    }
} 