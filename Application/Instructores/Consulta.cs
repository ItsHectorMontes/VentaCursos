using Infrastructure.DapperConexion.Instructor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Instructores
{
    public class Consulta
    {
        public class Lista : IRequest<List<InstructorModel>>
        {

        }
        public class Manejador : IRequestHandler<Lista, List<InstructorModel>>
        {
            private readonly IInstructor _instructorRepository;
            public Manejador(IInstructor intructorRepository)
            {
                _instructorRepository = intructorRepository;
            }
            public async Task<List<InstructorModel>> Handle(Lista request, CancellationToken cancellationToken)
            {
                var resultado = await _instructorRepository.ObtenerLista();
                return resultado.ToList();
                
            }
        }
    }
}
