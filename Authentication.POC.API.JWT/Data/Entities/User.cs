using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.POC.API.JWT.Data.Entities
{
    [Table("User")]
    public class User : BaseEntity
    {
        [Required]
        public string Email { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;

        public bool Enabled { get; set; } = true;

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        public Role? Role { get; set; }
    }
}
