using System;
using System.Threading;
using System.Threading.Tasks;
using GoalPost.Core.Entities;
using GoalPost.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GoalPost.Application.Features.Auth.Commands
{
    public record LoginCommand : IRequest<(bool success, string token, string error)>
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, (bool success, string token, string error)>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;

        public LoginCommandHandler(
            IUserRepository userRepository,
            IAuthService authService,
            UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _authService = authService;
            _userManager = userManager;
        }

        public async Task<(bool success, string token, string error)> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                return (false, string.Empty, "Felaktig e-postadress eller lösenord");
            }

            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                return (false, string.Empty, "Felaktig e-postadress eller lösenord");
            }

            if (!user.IsActive)
            {
                return (false, string.Empty, "Kontot är inaktiverat");
            }

            // Uppdatera LastLoginAt
            user.LastLoginAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);

            var token = _authService.GenerateJwtToken(user);
            return (true, token, string.Empty);
        }
    }
} 