using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Security
{
    public class RolLista
    {
        public class Ejecuta : IRequest<List<IdentityRole>>
        {

        }

        public class Manejador : IRequestHandler<Ejecuta, List<IdentityRole>>
        {
            private readonly ApplicationDbContextSeed _applicationDbContextSeed;
            public Manejador(ApplicationDbContextSeed applicationDbContextSeed)
            {
                _applicationDbContextSeed = applicationDbContextSeed;
            }

            public async Task<List<IdentityRole>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var roles = await _applicationDbContextSeed.Roles.ToListAsync();
                return roles;
            }
        }
    }
}
