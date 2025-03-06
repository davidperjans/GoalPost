using MediatR;
using GoalPost.Application.DTOs.Auth;

namespace GoalPost.Application.Features.Auth.Commands.RegisterUser;

public class RegisterUserCommand : IRequest<AuthResponseDto>
{
    public RegisterDto RegisterDto { get; set; } = null!;
} 