namespace GoalPost.Application.DTOs.Auth
{
    public class AuthResponse
    {
        public string? Token { get; set; }
        public UserDto? User { get; set; }
        public string? Error { get; set; }
    }
} 