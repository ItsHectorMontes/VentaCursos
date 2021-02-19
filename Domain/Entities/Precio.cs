using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Precio
    {
        public int PrecioId { get; set; }
        public decimal PrecioActual { get; set; }
        public decimal PrecioPromocion { get; set; }
        public int CursoId { get; set; }
        public Curso Curso { get; set; }

    }
}
