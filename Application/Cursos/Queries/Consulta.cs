using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cursos.Queries
{
    public class Consulta
    {
        public class ListaCursos : IRequest<List<Curso>>
        {

        }
        public class Manejador : IRequestHandler<ListaCursos, List<Curso>>
        {
            private readonly ApplicationDbContextSeed _contextSeed;
            public Manejador(ApplicationDbContextSeed contextSeed)
            {
                _contextSeed = contextSeed;

            }
            public async Task<List<Curso>> Handle(ListaCursos request, CancellationToken cancellationToken)
            {
                var  cursos = await _contextSeed.Curso.ToListAsync();
                return cursos;
            }
        }
    }
}
