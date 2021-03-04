using Application.Cursos.Queries.DtoCourse;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //agregando mapeo manual
            CreateMap<Curso, CursoDto>()
                 .ForMember(x => x.Instructores, y => y.MapFrom(z => z.InstructoresLink.Select(a => a.Instructor).ToList()))
                 .ForMember(x => x.Comentarios, y => y.MapFrom(z => z.ComentarioLista))
                 .ForMember(x => x.Precio, y => y.MapFrom(z => z.PrecioPromocion));

            CreateMap<CursoInstructor, CursoInstructorDto>();
            CreateMap<Instructor, InstructorDto>();
            CreateMap<Comentario, ComentarioDto>();
            CreateMap<Precio, PrecioDto>();

                
        }
    }
}
