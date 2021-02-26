using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IJwtGenerador
    {
        string CrearToken(Usuario usuario);
    }
}
