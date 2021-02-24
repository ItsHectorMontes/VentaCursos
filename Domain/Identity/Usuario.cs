using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Identity
{
    public class Usuario : IdentityUser
    {
        public string NombreCompleto { get; set; }

    }
}
