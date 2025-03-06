using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace GoalPost.Core.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public required string DisplayName { get; set; }

        public string? ProfileImage { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastLoginAt { get; set; }

        // Navigation properties
        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; } = new List<IdentityUserRole<string>>();
    }
} 