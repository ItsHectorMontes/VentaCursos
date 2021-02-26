using Application.Interfaces;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Security
{
    public class UsuarioActual
    {
        public class Ejecutar : IRequest<UsuarioData>
        {

        }
        public class Manejador : IRequestHandler<Ejecutar, UsuarioData>
        {
            private readonly UserManager<Usuario> _usermanager;
            private readonly IJwtGenerador _jwtGenerador;
            private readonly IUsuarioSesion _usuarioSesion;
            public Manejador(UserManager<Usuario> userManager, IJwtGenerador jwtGenerador, IUsuarioSesion usuariosesion)
            {
                _usermanager = userManager;
                _jwtGenerador = jwtGenerador;
                _usuarioSesion = usuariosesion;
            }
            
            public async Task<UsuarioData> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var usuario = await _usermanager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());
                return new UsuarioData
                {
                    NombreCompleto = usuario.NombreCompleto,
                    UserName = usuario.UserName,
                    Token = _jwtGenerador.CrearToken(usuario),
                    Imagen = null,
                    Email = usuario.Email

                };
            }
        }

    }
}
