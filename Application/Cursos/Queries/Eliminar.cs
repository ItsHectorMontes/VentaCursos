using Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cursos.Queries
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public int Id { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ApplicationDbContextSeed _contextSeed;
            public Manejador(ApplicationDbContextSeed contextSeed)
            {
                _contextSeed = contextSeed;

            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso =  await _contextSeed.Curso.FindAsync(request.Id);
                if (curso==null)
                {
                    throw new Exception("No se puede eliminar el curso");
                }
                _contextSeed.Remove(curso);

                var resultado = await _contextSeed.SaveChangesAsync();
                if (resultado>0)
                {
                    return Unit.Value;
                }
                throw new Exception("no se guardo los cambios");

                
            }
        }
    }
}
