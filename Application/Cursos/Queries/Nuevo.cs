using Domain.Entities;
using FluentValidation;
using Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            public DateTime? FechaPublicacion { get; set; }
            
            public List<Guid> ListaInstructor { get; set; }
            public decimal Precio { get; set; }
            public decimal Promocion { get; set; }
        }
        /// <summary>
        /// validacion using fluent validation
        /// </summary>
        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.Descripcion).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
            }
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
                Guid _cursoId = Guid.NewGuid();
                var curso = new Curso
                {
                    CursoId = _cursoId,
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion,
                    FechaCreacion = DateTime.UtcNow
                };
                _contextSeed.Curso.Add(curso);

                if (request.ListaInstructor != null)
                {
                    foreach(var id in request.ListaInstructor)
                    {
                        var cursoInstructor = new CursoInstructor
                        {
                            CursoId = _cursoId,
                            InstructorId = id
                        };
                        _contextSeed.CursoInstructor.Add(cursoInstructor);
                    }
                }
                //agregar logica para insertar un precio del curso.

                var precioEntidad = new Precio
                {
                    CursoId = _cursoId,
                    PrecioActual = request.Precio,
                    PrecioPromocion = request.Promocion,
                    PrecioId = Guid.NewGuid()
                };
                _contextSeed.Precio.Add(precioEntidad);

                //guardando en la BD (savechangesasync)
                var valor = await _contextSeed.SaveChangesAsync();
                if (valor>0)                
                    return Unit.Value;                
                throw new Exception("No se puedo insertar el curso");
            }
        }
    }
}
