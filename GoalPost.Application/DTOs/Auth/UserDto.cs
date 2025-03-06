using System;
using System.Collections.Generic;

namespace GoalPost.Application.DTOs.Auth
{
    public class UserDto
    {
        public required string Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public string? ProfileImage { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public required ICollection<string> Roles { get; set; }
    }
} 