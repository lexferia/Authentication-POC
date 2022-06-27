using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.POC.Web.Shared.DTOs
{
    public abstract class BaseDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
