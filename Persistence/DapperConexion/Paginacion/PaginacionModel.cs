using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DapperConexion.Paginacion
{
    public class PaginacionModel
    {
        public List<IDictionary<string,object>>ListaRecords { get; set; }
        //retorna json , [{curdoid: "541352132", "titulo" : "curso1}]
        public int TotalRecords { get; set; }
        public int NumeroPaginas { get; set; }


    }
}
