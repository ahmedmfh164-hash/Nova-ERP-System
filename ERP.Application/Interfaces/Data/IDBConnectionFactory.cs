using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace ERP.Application.Interfaces.Data
{
    public interface IDBConnectionFactory
    {
        public Task<SqlConnection> CreateConnectionAsync();


    }
}
