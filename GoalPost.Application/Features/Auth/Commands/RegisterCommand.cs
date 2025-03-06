using System;
using System.Threading;
using System.Threading.Tasks;
using GoalPost.Core.Entities;
using GoalPost.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GoalPost.Application.Features.Auth.Commands
{
    public record RegisterCommand : IRequest<(bool success, string token, string error)>
    {
        public required string Username { get; init; }
        public required string Email { get; init; }
        public required string Password { get; init; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, (bool success, string token, string error)>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;

        public RegisterCommandHandler(
            IUserRepository userRepository,
            IPasswordService passwordService,
            IAuthService authService,
            UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _authService = authService;
            _userManager = userManager;
        }

        public async Task<(bool success, string token, string error)> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Kontrollera om användaren redan finns
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return (false, string.Empty, "E-postadressen är redan registrerad");
            }

            existingUser = await _userRepository.GetByUsernameAsync(request.Username);
            if (existingUser != null)
            {
                return (false, string.Empty, "Användarnamnet är redan taget");
            }

            // Skapa ny användare
            var user = new User
            {
                UserName = request.Username,
                Email = request.Email,
                DisplayName = request.Username,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return (false, string.Empty, string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            // Generera token
            var token = _authService.GenerateJwtToken(user);

            return (true, token, string.Empty);
        }
    }
} 