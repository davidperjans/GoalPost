using Microsoft.AspNetCore.Identity;
using GoalPost.Core.Domain;

namespace GoalPost.Core.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(string id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUserNameAsync(string userName);
    Task<IEnumerable<User>> GetAllAsync();
    Task<IdentityResult> CreateAsync(User user, string password);
    Task<IdentityResult> UpdateAsync(User user);
    Task<IdentityResult> DeleteAsync(User user);
    Task<bool> ExistsAsync(string email);
    Task<bool> ExistsAsync(string email, string userName);
} 