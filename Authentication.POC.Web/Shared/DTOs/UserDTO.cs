using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.POC.Web.Shared.DTOs
{
    public class UserDTO : BaseDTO
    { 
        public string Email { get; set; } = default!;

        public bool Enabled { get; set; }

        public Guid RoleId { get; set; }

        public string RoleName { get; set; } = default!;
    }
}
