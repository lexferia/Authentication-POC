﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.POC.Web.Shared.DTOs
{
    public class AddorEditRoleDTO
    {
        [Required]
        public string Name { get; set; } = default!;
    }
}
