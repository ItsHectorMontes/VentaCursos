using Application.Cursos.Queries.DtoCourse;
using Application.ManejadorError.Exceptions;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cursos.Queries
{
    public class ConsultaId
    {
        public class CursoUnico : IRequest<CursoDto>
        {
            public Guid Id { get; set; }

        }
        public class Manejador : IRequestHandler<CursoUnico, CursoDto>

        {
            private readonly ApplicationDbContextSeed _contextSeed;
            private readonly IMapper _mapper;
            public Manejador(ApplicationDbContextSeed contextSeed, IMapper mapper)
            {
                _contextSeed = contextSeed;
                _mapper = mapper;
            }

            public async Task<CursoDto> Handle(CursoUnico request, CancellationToken cancellationToken)
            {
                var curso = await _contextSeed.Curso
                    .Include(x => x.ComentarioLista)
                    .Include(x => x.PrecioPromocion)
                    .Include(x => x.InstructoresLink)
                    .ThenInclude(y => y.Instructor)
                    .FirstOrDefaultAsync(a => a.CursoId == request.Id);
                    
                if (curso == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { curso = "No se encontro el Curso" });
                }
                var cursoDto = _mapper.Map<Curso, CursoDto>(curso);

                return cursoDto;
            }
        }
    }
}
