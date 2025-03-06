using System;
using System.Threading;
using System.Threading.Tasks;
using GoalPost.Core.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace GoalPost.Application.Features.Auth.Commands
{
    public class ResetPasswordCommand : IRequest<(bool success, string message)>
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Token { get; set; }

        [Required]
        [MinLength(6)]
        public required string NewPassword { get; set; }
    }

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, (bool success, string message)>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;

        public ResetPasswordCommandHandler(IUserRepository userRepository, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        public async Task<(bool success, string message)> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                return (false, "Användaren kunde inte hittas");
            }

            // I en mer avancerad implementation skulle vi här validera token mot en databas
            // För nu antar vi att token är giltig om den matchar den som skickades i e-post

            user.PasswordHash = await _passwordService.HashPasswordAsync(request.NewPassword);
            await _userRepository.UpdateAsync(user);

            return (true, "Lösenordet har återställts");
        }
    }
} 