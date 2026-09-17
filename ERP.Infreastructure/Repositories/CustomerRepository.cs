using ERP.Application.Interfaces.Data;
using ERP.Application.Interfaces.Helpers;
using ERP.Contacts.Requests.Customer;
using ERP.Core.Enums;
using ERP.Domain.Entities;
using ERP.Infreastructure.Extentions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using ERP.Application.Interfaces.Repositories;

namespace ERP.Infreastructure.Repositories
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly IDBConnectionFactory _DbConnectionFactory;
        private readonly IStoredProcedtureExecutor _StoredProcedture;

        public CustomerRepository(IDBConnectionFactory dbConnectionFactory,
        IStoredProcedtureExecutor storedProcedureExecutor)
        {
            _DbConnectionFactory = dbConnectionFactory;
            _StoredProcedture = storedProcedureExecutor;
        }


        private Customer MapToCustomer(SqlDataReader reader)
        {
            return new Customer
            (
                customerId: reader.GetInt32(reader.GetOrdinal("CustomerId")),
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

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            List<Customer> list = new List<Customer>();

            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("sp_GetAllCustomers", con);

            list= await _StoredProcedture.ExecuteListAsync(cmd, con, MapToCustomer);

            return list;
        }

        public async Task<int> AddCustomerAsync(CreatedCustomerDTO customerDTO)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("sp_AddNewCustomer", con);

            SqlCommandExtentions.AddParameters(cmd, customerDTO);

            int customerId = await _StoredProcedture.ExecuteScalarAsync(cmd, con);

            return customerId;
        }


        public async Task<int> DeleteCustomerAsync(int customerId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("sp_DeleteCustomer", con);

            SqlCommandExtentions.AddParameters(cmd, "@CustomerId", customerId);

            return await _StoredProcedture.ExecuteNonQueryAsync(cmd, con);

        }


        public async Task<Customer> GetCustomerByCustomerIdAsync(int customerId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("sp_GetCustomerByCustomerId", con);

            SqlCommandExtentions.AddParameters(cmd, "@CustomerId", customerId);

            Customer customer = await _StoredProcedture.ExecuteSingleAsync(cmd, con, MapToCustomer);

            return customer;
        }


        public async Task<bool> isCustomerExistAsync(int customerId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("sp_isCustomerExist", con);

            SqlCommandExtentions.AddParameters(cmd, "@CustomerId", customerId);

            bool isFound = await _StoredProcedture.ExecuteBooleenAsync(cmd, con);

            return isFound;
        }

    }
}
