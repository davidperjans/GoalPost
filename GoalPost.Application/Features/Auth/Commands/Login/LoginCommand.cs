using MediatR;
using GoalPost.Application.DTOs.Auth;

namespace GoalPost.Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<AuthResponseDto>
{
    public LoginDto LoginDto { get; set; } = null!;
}