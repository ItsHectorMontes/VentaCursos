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
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {            
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime FechaPublicacion { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta>

        {
            //llamando al contexto para hacer uso de los datos.
            private readonly ApplicationDbContextSeed _contextSeed;
            public Manejador(ApplicationDbContextSeed contextSeed)
            {
                _contextSeed = contextSeed;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso = new Curso
                {
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion
                };
                _contextSeed.Curso.Add(curso);
                var valor = await _contextSeed.SaveChangesAsync();
                if (valor>0)                
                    return Unit.Value;                
                throw new Exception("No se puedo insertar el curso");
            }
        }
    }
}
