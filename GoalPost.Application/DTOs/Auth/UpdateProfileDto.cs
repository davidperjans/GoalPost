using System.ComponentModel.DataAnnotations;

namespace GoalPost.Application.DTOs.Auth;

public class UpdateProfileDto
{
    [StringLength(50)]
    public string? UserName { get; set; }

    [StringLength(250)]
    public string? Bio { get; set; }

    [Url]
    public string? ProfileImageUrl { get; set; }

    [StringLength(100, MinimumLength = 8)]
    public string? CurrentPassword { get; set; }

    [StringLength(100, MinimumLength = 8)]
    public string? NewPassword { get; set; }

    [Compare("NewPassword")]
    public string? ConfirmNewPassword { get; set; }
} 