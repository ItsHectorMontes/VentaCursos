using Domain.Entities;
using FluentValidation;
using Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Comentarios
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public Guid ComentarioID { get; set; }
            public string Alumno { get; set; }
            public int Puntaje { get; set; }
            public string ComentarioTexto { get; set; }
            public Guid CursoId { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Alumno).NotEmpty();
                RuleFor(x => x.Puntaje).NotEmpty();
                RuleFor(x => x.ComentarioTexto).NotEmpty();
                RuleFor(x => x.CursoId).NotEmpty();

            }
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
                var comentario = new Comentario
                {
                    ComentarioId = Guid.NewGuid(),
                    Alumno = request.Alumno,
                    Puntaje = request.Puntaje,
                    ComentarioTexto = request.ComentarioTexto,
                    CursoId = request.CursoId,
                    FechaCreacion = DateTime.UtcNow
                };
                _contextSeed.Comentario.Add(comentario);

                var resultado = await _contextSeed.SaveChangesAsync();

                if (resultado>0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el comentario");

            }
        }
    }
}
