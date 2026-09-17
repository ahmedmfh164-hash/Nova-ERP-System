using ERP.Application.Interfaces.Data;
using ERP.Application.Interfaces.Helpers;
using ERP.Application.Interfaces.Repositories;
using ERP.Contacts.Requests.Supplier;
using ERP.Contacts.Responses;
using ERP.Core.Enums;
using ERP.Domain.Entities;
using ERP.Infreastructure.Extentions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;


namespace ERP.Infreastructure.Repositories
{
    public class SupplierRepository  :ISupplierRepository
    {
        private readonly IDBConnectionFactory _DbConnectionFactory;
        private readonly IStoredProcedtureExecutor _StoredProcedture;

        public SupplierRepository(IDBConnectionFactory dbConnectionFactory,
        IStoredProcedtureExecutor storedProcedureExecutor)
        {
            _DbConnectionFactory = dbConnectionFactory;
            _StoredProcedture = storedProcedureExecutor;
        }


        private Supplier MapToSupplier(SqlDataReader reader)
        {
            return new Supplier
            (
                supplierId: reader.GetInt32(reader.GetOrdinal("SupplierId")) ,
                person: new Person
            (
                PersonId: reader.GetInt32(reader.GetOrdinal("PersonId")),
                PersonName: reader.GetString(reader.GetOrdinal("PersonName")),
                Phone: reader.GetString(reader.GetOrdinal("Phone")),
                Email: reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                Address: reader.GetString(reader.GetOrdinal("Address")),
                Gender: (Gender)reader.GetOrdinal("Gender"),
                ImageGuid: reader.IsDBNull(reader.GetOrdinal("ImageGuid")) ? null : reader.GetGuid(reader.GetOrdinal("ImageGuid"))
            )
            );
       
        }

        public async Task<List<Supplier>> GetAllSuppliersAsync()
        {
            List<Supplier> list = new List<Supplier>();

            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("sp_GetAllSuppliers", con);

            list= await _StoredProcedture.ExecuteListAsync(cmd, con, MapToSupplier);

            return list;
        }

        public async Task<int> AddSupplierAsync(CreatedSupplierDTO supplierDTO)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("sp_AddNewSupplier", con);

            SqlCommandExtentions.AddParameters(cmd, supplierDTO);

            int supplierId = await _StoredProcedture.ExecuteScalarAsync(cmd, con);

            return supplierId;
        }


        public async Task<int> DeleteSupplierAsync(int supplierId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("sp_DeleteSupplier", con);

            SqlCommandExtentions.AddParameters(cmd, "@SupplierId", supplierId);

            return await _StoredProcedture.ExecuteNonQueryAsync(cmd, con);

        }


        public async Task<Supplier> GetSupplierBySupplierIdAsync(int supplierId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("sp_GetSupplierBySupplierId", con);

            SqlCommandExtentions.AddParameters(cmd, "@SupplierId", supplierId);

            Supplier supplier = await _StoredProcedture.ExecuteSingleAsync(cmd, con, MapToSupplier);

            return supplier;
        }


        public async Task<bool> isSupplierExistAsync(int supplierId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("sp_isSupplierExist", con);

            SqlCommandExtentions.AddParameters(cmd, "@SupplierId", supplierId);

            bool isFound = await _StoredProcedture.ExecuteBooleenAsync(cmd, con);

            return isFound;
        }


    }
}
