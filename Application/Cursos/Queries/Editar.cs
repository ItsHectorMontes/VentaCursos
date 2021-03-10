using Application.ManejadorError.Exceptions;
using Domain.Entities;
using FluentValidation;
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
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public Guid CursoId { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public List<Guid> ListaInstructor { get; set; }
            public decimal? Precio { get; set; }
            public decimal? Promocion { get; set; }

        }
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
            private readonly ApplicationDbContextSeed _contextSeed;
            public Manejador(ApplicationDbContextSeed contextSeed)
            {
                _contextSeed = contextSeed;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso = await _contextSeed.Curso.FindAsync(request.CursoId);
                if (curso == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { curso = "No se encontro el Curso" });
                }

                curso.Titulo = request.Titulo ?? curso.Titulo;
                curso.Descripcion = request.Descripcion ?? curso.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion ?? curso.FechaPublicacion;
                curso.FechaCreacion = DateTime.UtcNow;

                ////actualizar el precio del curso
                var precioEntidad = _contextSeed.Precio.Where(x => x.CursoId == curso.CursoId).FirstOrDefault();
                if (precioEntidad != null)
                {
                    precioEntidad.PrecioPromocion = request.Promocion ?? precioEntidad.PrecioPromocion;
                    precioEntidad.PrecioActual = request.Precio ?? precioEntidad.PrecioActual;
                }
                else
                {
                    precioEntidad = new Precio
                    {
                        PrecioId = Guid.NewGuid(),
                        PrecioActual = request.Precio ?? 0,
                        PrecioPromocion = request.Promocion ?? 0,
                        CursoId = curso.CursoId
                    };
                    await _contextSeed.Precio.AddAsync(precioEntidad);
                }



                if (request.ListaInstructor!=null)
                {
                    if (request.ListaInstructor.Count>0)
                    {//eliminar los instructores actuales.
                        var instructoresBD = _contextSeed.CursoInstructor.Where(x => x.CursoId == request.CursoId);
                        foreach (var instructorEliminar in instructoresBD)
                        {
                            _contextSeed.CursoInstructor.Remove(instructorEliminar);
                        }//fin
                        //agregar instructor que viene del cliente
                        foreach (var id in request.ListaInstructor)
                        {
                            var nuevoInstructor = new CursoInstructor
                            {
                                CursoId = request.CursoId,
                                InstructorId = id
                            };
                            _contextSeed.CursoInstructor.Add(nuevoInstructor);   
                        }
                        //fin
                    }

                }
                var resultado = await _contextSeed.SaveChangesAsync();

                if (resultado>0)                
                    return Unit.Value;                
                throw new Exception("no se guardaron los cambios en el curso.");



            }
        }
    }
}
