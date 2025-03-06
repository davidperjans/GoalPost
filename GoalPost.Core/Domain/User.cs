using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace GoalPost.Core.Domain;

public class User : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public bool IsActive { get; set; }
    public string? Bio { get; set; }
    public string? ProfileImageUrl { get; set; }
} 