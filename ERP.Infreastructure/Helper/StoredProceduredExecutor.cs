using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ERP.Application.Interfaces.Helpers;
using ERP.Application.Interfaces.Data;
using ERP.Domain.Entities;
using System.Reflection;
namespace ERP.Infreastructure.Helper
{
    public class StoredProceduredExecutor:IStoredProcedtureExecutor
    {
        private readonly IDBConnectionFactory _ConnectionFactory;

        public async Task<SqlConnection> CreateConnectionAsync()
        {
            return await _ConnectionFactory.CreateConnectionAsync();
        }

        public SqlCommand CreateCommand(string spName,SqlConnection connection) 
        {
            return new SqlCommand(spName, connection)
            {
                CommandType=CommandType.StoredProcedure
            };

        }

       
        public async Task<List<T>> ExecuteListAsync<T>(SqlCommand cmd, SqlConnection conn,
       Func<SqlDataReader, T> mapFunc)
        { 

            var results = new List<T>();
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    results.Add(mapFunc(reader));
                }
            }

            return results;
        }

        public async Task<T> ExecuteSingleAsync<T>(SqlCommand cmd, SqlConnection conn,
      Func<SqlDataReader, T> mapFunc)
        {
            T? result = default;
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    result=mapFunc(reader);
                }
            }

            return result;
        }

        public async Task<int> ExecuteScalarAsync(SqlCommand cmd, SqlConnection con)
        {
            if (con.State!=ConnectionState.Open)
               await con.OpenAsync();

               object? result= await cmd.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }

        public async Task<int> ExecuteNonQueryAsync(SqlCommand cmd, SqlConnection conn)
        {
            int RowsAffected = 0;
            RowsAffected=await cmd.ExecuteNonQueryAsync();

            return RowsAffected;
        }

        public async Task<bool> ExecuteBooleenAsync(SqlCommand cmd, SqlConnection conn)
        {
            bool isFound = false;
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    isFound = true;
                }
            }

            return isFound;
        }

    }
}
