using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Cursos.Queries.DtoCourse
{
    public class PrecioDto
    {
        public Guid PrecioId { get; set; }        
        public decimal PrecioActual { get; set; }        
        public decimal PrecioPromocion { get; set; }
        public Guid CursoId { get; set; }
    }
}
