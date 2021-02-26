using Application.Interfaces;
using Application.ManejadorError.Exceptions;
using Domain.Identity;
using FluentValidation;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Security
{
    public class Registrar
    {
        public class Ejecuta : IRequest<UsuarioData>
        {
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string UserName { get; set; }

        }
        /// <summary>
        /// fluent valitacion for parameters of new user.
        /// </summary>
        public class EjecutaValidador : AbstractValidator<Ejecuta>
        {
            public EjecutaValidador()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellidos).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();
            }
        }
        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly ApplicationDbContextSeed _contextSeed;
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerador _jwtGenerador;

            public Manejador(ApplicationDbContextSeed contextSeed, UserManager<Usuario> userManager, IJwtGenerador jwtGenerador)
            {
                //inyectando variables
                _contextSeed = contextSeed;
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;

            }

            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //validacion para email
                var existe = await _contextSeed.Users.Where(x => x.Email == request.Email).AnyAsync();
                if (existe)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "Existe ya un usuario registrado con este email" });
                }
                //validacion para username
                var existeUserName = await _contextSeed.Users.Where(x => x.UserName == request.UserName).AnyAsync();
                if (existeUserName)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "Existe ya un usuario registrado con este Username" });
                }
                var usuario = new Usuario
                {
                    NombreCompleto = request.Nombre + " " + request.Apellidos,
                    Email = request.Email,
                    UserName = request.UserName

                };
                var resultado = await _userManager.CreateAsync(usuario, request.Password);
                if(resultado.Succeeded)
                {
                    return new UsuarioData
                    {
                        NombreCompleto = usuario.NombreCompleto,
                        Email = usuario.Email,
                        Token = _jwtGenerador.CrearToken(usuario),                       
                        UserName = usuario.UserName
                    };
                }
                throw new Exception("no se puedo agregar al nuevo usuario");

             }
        }
    }
}

