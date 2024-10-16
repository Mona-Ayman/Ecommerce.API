
using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public record RegisterDTO
    {
        [Required(ErrorMessage = "DisplayName ia required")]
        public string DisplayName { get; init; }
        [Required(ErrorMessage = "Email ia required")]
        [EmailAddress]
        public string Email { get; init; }
        [Required(ErrorMessage = "Password ia required")]
        public string Password { get; init; }
        [Required(ErrorMessage = "UserName ia required")]
        public string UserName { get; init; }
        public string? PhoneNumber { get; init; }
    }
}
