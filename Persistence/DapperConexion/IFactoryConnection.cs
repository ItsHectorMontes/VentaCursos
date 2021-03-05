using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastructure.DapperConexion
{
    public interface IFactoryConnection
    {
        void CloseConnection();
        IDbConnection GetConnection();
    }
}
