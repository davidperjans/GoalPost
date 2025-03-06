using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoalPost.Core.Entities;

namespace GoalPost.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(string email);
        Task<bool> ExistsAsync(string email, string username);
    }
} 