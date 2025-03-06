using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GoalPost.Core.Entities;
using GoalPost.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GoalPost.Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public PasswordService(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public async Task<string> HashPasswordAsync(string password)
        {
            var user = new User { UserName = "temp", DisplayName = "Temporary User" };
            return await Task.FromResult(_passwordHasher.HashPassword(user, password));
        }

        public async Task<bool> VerifyPasswordAsync(string password, string passwordHash)
        {
            var user = new User { UserName = "temp", DisplayName = "Temporary User" };
            return await Task.FromResult(_passwordHasher.VerifyHashedPassword(user, passwordHash, password) != PasswordVerificationResult.Failed);
        }

        public async Task<string> GeneratePasswordResetTokenAsync()
        {
            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            return token.Replace("/", "_").Replace("+", "-");
        }

        public async Task<bool> ValidatePasswordStrengthAsync(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            // Minst 8 tecken
            if (password.Length < 8)
                return false;

            // Minst en stor bokstav
            if (!password.Any(char.IsUpper))
                return false;

            // Minst en liten bokstav
            if (!password.Any(char.IsLower))
                return false;

            // Minst en siffra
            if (!password.Any(char.IsDigit))
                return false;

            // Minst ett specialtecken
            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                return false;

            return true;
        }

        public bool ValidatePassword(string password)
        {
            return password.Length >= 6;
        }
    }
} 