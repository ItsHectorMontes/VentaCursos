using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cursos.Queries
{
    public class ConsultaId
    {
        public class CursoUnico : IRequest<Curso>
        {
            public int Id { get; set; }

        }
        public class Manejador : IRequestHandler<CursoUnico, Curso>

        {
            private readonly ApplicationDbContextSeed _contextSeed;
            public Manejador(ApplicationDbContextSeed contextSeed)
            {
                _contextSeed = contextSeed;
            }

            public async Task<Curso> Handle(CursoUnico request, CancellationToken cancellationToken)
            {
                var curso = await _contextSeed.Curso.FindAsync(request.Id);
                return curso;
            }
        }
    }
}
