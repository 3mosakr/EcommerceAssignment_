using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Dto.User
{
    public class UserUpdateDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        // optional password field for updates
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        public string? Password { get; set; }
    }
}
