using ERP.Contacts.Requests.Users;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllUsersAsync();
        public Task<int> AddUserAsync(User user);
        public Task<int> EditUserInfoAsync(UpdatedUserDTO user);
        public Task<int> DeleteUserAsync(int userId);
        public Task<User> GetUserByUserIdAsync(int userId);
        public Task<User> GetUserByEmailAsync(string email);
        public Task<bool> isUserExistAsync(int userId);



    }
}
