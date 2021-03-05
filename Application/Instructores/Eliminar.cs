using Infrastructure.DapperConexion.Instructor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Instructores
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public Guid Id { get; set; }

        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly IInstructor _instructorRepositorio;
            public Manejador(IInstructor instructorRepositorio)
            {
                _instructorRepositorio = instructorRepositorio;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var resultados = await _instructorRepositorio.Eliminar(request.Id);
                if (resultados>0)
                {
                    return Unit.Value;
                }
                throw new Exception("no se puedo eliminar el instructor");

            }
        }
    }
}
