using Application.ManejadorError.Exceptions;
using Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cursos.Queries
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public Guid Id { get; set; }
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
                var instructoresDB = _contextSeed.CursoInstructor.Where(x => x.CursoId == request.Id);
                foreach (var instructor in instructoresDB)
                {
                    _contextSeed.CursoInstructor.Remove(instructor);
                }
                //eliminar precio y comentarios
                var comentariosDb = _contextSeed.Comentario.Where(x => x.CursoId == request.Id);
                foreach (var cmt in comentariosDb)
                {
                    _contextSeed.Comentario.Remove(cmt);
                }
                var precioDB = _contextSeed.Precio.Where(x => x.CursoId == request.Id).FirstOrDefault();
                if (precioDB!=null)
                {
                    _contextSeed.Precio.Remove(precioDB);
                }






                var curso =  await _contextSeed.Curso.FindAsync(request.Id);
                if (curso == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new {curso = "No se encontro el Curso" });
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
