using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Application.ManejadorError.Exceptions
{
    public class ManejadorExcepcion : Exception
    {       
        public HttpStatusCode Codigo { get; }
        public object Errores { get; }
        public ManejadorExcepcion(HttpStatusCode codigo, object errores = null)
        {
            Codigo = codigo;
            Errores = errores;
        }

    }
}
