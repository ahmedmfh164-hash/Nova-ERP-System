using ERP.Application.Interfaces.Data;
using ERP.Application.Interfaces.Helpers;
using ERP.Application.Interfaces.Repositories;
using ERP.Core;
using ERP.Core.Enums;
using ERP.Domain.Entities;
using ERP.Infreastructure.Extentions;
using Microsoft.Data.SqlClient;

namespace ERP.Infreastructure.Repositories
{
    public class PeopleRepository :IPeopleRepository
    {
       private readonly IDBConnectionFactory _DbConnectionFactory;
        private readonly IStoredProcedtureExecutor _StoredProcedture;

        public PeopleRepository(IDBConnectionFactory dbConnectionFactory,
        IStoredProcedtureExecutor storedProcedureExecutor)
        {
            _DbConnectionFactory = dbConnectionFactory;
            _StoredProcedture = storedProcedureExecutor;
        }

        private Person MapToPerson(SqlDataReader reader)
        {
            return new Person
            (
                PersonId: reader.GetInt32(reader.GetOrdinal("PersonId")),
                PersonName: reader.GetString(reader.GetOrdinal("PersonName")),
                Phone: reader.GetString(reader.GetOrdinal("Phone")),
                Email: reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")) ,
                Address: reader.GetString(reader.GetOrdinal("Address")),
                Gender: (Gender)reader.GetByte(reader.GetOrdinal("Gender")),
                ImageGuid: reader.IsDBNull(reader.GetOrdinal("ImageGuid")) ? null : reader.GetGuid(reader.GetOrdinal("ImageGuid"))
            );
        }

        private DeletedPersonResult MapToDelete(SqlDataReader reader)
        {
            return new DeletedPersonResult
            (
                personId: reader.GetInt32(reader.GetOrdinal("PersonId")),
                imageGuid: reader.IsDBNull(reader.GetOrdinal("ImageGuid")) ? null : reader.GetGuid(reader.GetOrdinal("ImageGuid"))
            );
        }

        public async Task<List<Person>> GetAllPeopleAsync()
        {
            List<Person> list = new List<Person>();

         await using var con= await _DbConnectionFactory.CreateConnectionAsync();

           await using var cmd = _StoredProcedture.CreateCommand("sp_GetAllPeople",con) ;
            
            list= await _StoredProcedture.ExecuteListAsync(cmd,con,MapToPerson);

          return list;
        }

        public async Task<int> AddPersonAsync(Person person)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("sp_AddNewPerson", con);

            SqlCommandExtentions.AddParameters(cmd, person);

            int personId = await _StoredProcedture.ExecuteScalarAsync(cmd, con);

            return personId;
        }

        public async Task<bool> EditPersonInfoAsync(Person updatedPerson)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("sp_UpdatePerson", con);

            SqlCommandExtentions.AddParameters(cmd, updatedPerson);

            return (await _StoredProcedture.ExecuteNonQueryAsync(cmd, con)>0);

        }

        public async Task<DeletedPersonResult?> DeletePersonAsync(int personId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("sp_DeletePerson", con);

            SqlCommandExtentions.AddParameters(cmd,"@PersonId",personId);

            DeletedPersonResult deletedPerson = await _StoredProcedture.ExecuteSingleAsync(cmd, con, MapToDelete);
           
            return deletedPerson;
        }


        public async Task<Person?> GetPersonByPersonIdAsync(int personId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("sp_GetPersonByPersonId", con);

            SqlCommandExtentions.AddParameters(cmd,"@PersonId", personId);

            Person person = await _StoredProcedture.ExecuteSingleAsync(cmd, con, MapToPerson);

            return person;
        }


        public async Task<bool> isPersonExistAsync(int personId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("sp_isPersonExistbyId", con);

            SqlCommandExtentions.AddParameters(cmd, "@PersonId", personId);

            bool isFound = await _StoredProcedture.ExecuteBooleenAsync(cmd, con);

            return isFound;
        }

        public async Task<bool> isPersonExistAsync(string email)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("sp_isPersonExistByEmail", con);

            SqlCommandExtentions.AddParameters(cmd, "@Email", email);

            bool isFound = await _StoredProcedture.ExecuteBooleenAsync(cmd, con);

            return isFound;
        }


    }

}
