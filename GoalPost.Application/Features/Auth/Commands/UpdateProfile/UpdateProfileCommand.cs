using MediatR;
using GoalPost.Application.DTOs.Auth;

namespace GoalPost.Application.Features.Auth.Commands.UpdateProfile;

public class UpdateProfileCommand : IRequest<AuthResponseDto>
{
    public string UserId { get; set; } = string.Empty;
    public UpdateProfileDto UpdateProfileDto { get; set; } = null!;
} 