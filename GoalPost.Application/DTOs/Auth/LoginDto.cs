using System.ComponentModel.DataAnnotations;

namespace GoalPost.Application.DTOs.Auth;

public class LoginDto
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; }
} 