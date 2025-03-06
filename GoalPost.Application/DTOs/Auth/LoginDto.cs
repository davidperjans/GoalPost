using System.ComponentModel.DataAnnotations;

namespace GoalPost.Application.DTOs.Auth
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Lösenordet måste vara minst 6 tecken")]
        public required string Password { get; set; }
    }
} 