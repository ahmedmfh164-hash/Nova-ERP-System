using ERP.Application.Interfaces.Data;
using ERP.Application.Interfaces.Helpers;
using ERP.Application.Interfaces.Repositories;
using ERP.Contacts.Requests.Users;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;
using ERP.Infreastructure.Extentions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Infreastructure.Repositories
{
    public class UserRepository: IUserRepository
    {
        private static IDBConnectionFactory _DbConnectionFactory;
        private readonly IStoredProcedtureExecutor _StoredProcedture;

        public UserRepository(IDBConnectionFactory dbConnectionFactory,
       IStoredProcedtureExecutor storedProcedureExecutor)
        {
            _DbConnectionFactory = dbConnectionFactory;
            _StoredProcedture = storedProcedureExecutor;
        }


        private User MapToUser(SqlDataReader reader)
        {
            return new User
            (
                userId: reader.GetInt32(reader.GetOrdinal("UserId")),
                personId: reader.GetInt32(reader.GetOrdinal("PersonId")),
                userName: reader.GetString(reader.GetOrdinal("UserName")),
                password:reader.GetString(reader.GetOrdinal("Password")),
                roleName:reader.GetString(reader.GetOrdinal("RoleName")),
                isActive:reader.GetBoolean(reader.GetOrdinal("IsActive"))
            );
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            List<User> list = new List<User>();

            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("sp_GetAllUsers", con);

            list= await _StoredProcedture.ExecuteListAsync(cmd, con, MapToUser);

            return list;
        }

        public async Task<int> AddUserAsync(User user)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("sp_AddNewUser", con);

            SqlCommandExtentions.AddParameters(cmd, user);

            int userId = await _StoredProcedture.ExecuteScalarAsync(cmd, con);

            return userId;
        }

        public async Task<int> EditUserInfoAsync(UpdatedUserDTO user)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("sp_UpdateUser", con);

            SqlCommandExtentions.AddParameters(cmd, user);

            return await _StoredProcedture.ExecuteNonQueryAsync(cmd, con);

        }

        public async Task<int> DeleteUserAsync(int userId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("sp_DeleteUser", con);

            SqlCommandExtentions.AddParameters(cmd, "@UserId", userId);

            return await _StoredProcedture.ExecuteNonQueryAsync(cmd, con);

        }


        public async Task<User> GetUserByUserIdAsync(int userId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("sp_GetUserByUserId", con);

            SqlCommandExtentions.AddParameters(cmd, "@UserId", userId);

            User user = await _StoredProcedture.ExecuteSingleAsync(cmd, con, MapToUser);

            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("sp_GetUserByEmail", con);

            SqlCommandExtentions.AddParameters(cmd, "@email", email);

            User user = await _StoredProcedture.ExecuteSingleAsync(cmd, con, MapToUser);

            return user;
        }


        public async Task<bool> isUserExistAsync(int userId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("sp_isUserExist", con);

            SqlCommandExtentions.AddParameters(cmd, "@UserId", userId);

            bool isFound = await _StoredProcedture.ExecuteBooleenAsync(cmd, con);

            return isFound;
        }

    }
}
