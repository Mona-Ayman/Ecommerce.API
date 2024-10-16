
using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public record LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; }
        [Required]
        public string Password { get; init; }
    }
}
