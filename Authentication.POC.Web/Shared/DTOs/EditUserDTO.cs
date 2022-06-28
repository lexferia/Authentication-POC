using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.POC.Web.Shared.DTOs
{
    public class EditUserDTO : AddUserDTO
    {
        public bool Enabled { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
