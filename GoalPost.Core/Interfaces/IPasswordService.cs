using System.Threading.Tasks;

namespace GoalPost.Core.Interfaces
{
    public interface IPasswordService
    {
        Task<string> HashPasswordAsync(string password);
        Task<bool> VerifyPasswordAsync(string password, string passwordHash);
        bool ValidatePassword(string password);
    }
} 