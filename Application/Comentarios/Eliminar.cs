using Application.ManejadorError.Exceptions;
using Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Comentarios
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
                var comentario = await _contextSeed.Comentario.FindAsync(request.Id);
                if (comentario==null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "no se encontro el comentario" });
                }
               _contextSeed.Remove(comentario);
                var resultado = await _contextSeed.SaveChangesAsync();
                if (resultado>0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se puedo eliminar el comentario");

            }
        }
    }

}
