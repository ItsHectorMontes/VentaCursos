using Application.Cursos.Queries.DtoCourse;
using AutoMapper;
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
        public class ListaCursos : IRequest<List<CursoDto>>
        {

        }
        public class Manejador : IRequestHandler<ListaCursos, List<CursoDto>>
        {
            private readonly ApplicationDbContextSeed _contextSeed;
            private readonly IMapper _mapper;
            public Manejador(ApplicationDbContextSeed contextSeed, IMapper mapper)
            {
                _contextSeed = contextSeed;
                _mapper = mapper;    
            }
            public async Task<List<CursoDto>> Handle(ListaCursos request, CancellationToken cancellationToken)
            {
                var  cursos = await _contextSeed.Curso
                    .Include(x => x.ComentarioLista)
                    .Include(x => x.PrecioPromocion)
                    .Include(x => x.InstructoresLink)
                    .ThenInclude(x => x.Instructor).ToListAsync();
                //mapeando curso a cursodto.
                var cursosDto = _mapper.Map<List<Curso>, List<CursoDto>>(cursos);
                return cursosDto;
            }
        }
    }
}
