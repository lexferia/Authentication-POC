using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.POC.Web.Shared.DTOs
{
    public class AddUserDTO
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string Email { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;

        [Required]
        public Guid RoleId { get; set; }
    }
}
