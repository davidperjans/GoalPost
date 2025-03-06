using System;
using System.Threading.Tasks;
using GoalPost.Core.Entities;

namespace GoalPost.Core.Interfaces
{
    public interface IAuthService
    {
        Task<(bool success, string token)> ValidateCredentialsAsync(string email, string password);
        string GenerateJwtToken(User user);
        bool ValidateToken(string token);
        Task<User?> GetUserFromTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
        Task<bool> IsUserActiveAsync(Guid userId);
    }
} 