using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace RSIapi.DTOs
{
    public class RegisterUserDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]

        public string Name { get; set; }

        public int Age { get; set; }

        public int RoleId { get; set; } = 2;
    }
}
