using Application.ManejadorError.Exceptions;
using Domain.Identity;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Security
{
    public class UsuarioRolAgregar
    {
        public class Ejecuta : IRequest
        {
            public string UserName { get; set; }
            public string RolNombre { get; set; }
        }
        public class EjecutaValidador : AbstractValidator<Ejecuta>
        {
            public EjecutaValidador()
            {
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.RolNombre).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;

            public Manejador(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.RolNombre);
                if (role == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { message = "El Rol no existe" });
                }
                var usuarioIden = _userManager.FindByNameAsync(request.UserName);
                if (usuarioIden == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { message = "El Usuario no existe" });
                }

                var resultado =await _userManager.AddToRoleAsync(await usuarioIden, request.RolNombre);
                if (resultado.Succeeded)
                {
                    return Unit.Value;
                }
                throw new Exception("no se puedo agregar el rol al usuario.");
            }
        }
    }
}
