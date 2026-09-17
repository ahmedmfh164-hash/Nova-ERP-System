using ERP.Application.Interfaces.Data;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Mappings;
using ERP.Core;
using ERP.Contacts.Requests.Users;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;


namespace ERP.Application.Servicies
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository repo)
        {
            _userRepository = repo;
        }


        public async Task<List<UserResponseDTO>> GetAllUsersAsync()
        {
            List<User> Users = await _userRepository.GetAllUsersAsync();

            return Users.Select(user => user.ToResponseDTO()).ToList();

        }

        public async Task<int?> AddUserAsync(CreatedUserDTO dto)
        {
            var user=dto.ToEntity();
            user.Password=PasswordHasher.Hash(user.Password);

            int? userId = await _userRepository.AddUserAsync(user);
                                                      
            return userId;
        }

        public async Task<bool> EditUserInfoAsync(UpdatedUserDTO userDTO)
        {
            userDTO.Password = PasswordHasher.Hash(userDTO.Password);
            return (await _userRepository.EditUserInfoAsync(userDTO)>0);
        }

        public async Task<UserResponseDTO?> GetUserByUserIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByUserIdAsync(userId);

            return user == null ? null: user.ToResponseDTO();
        }

        public async Task<UserResponseDTO?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            return user == null ? null : user.ToResponseDTO();
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var rowAffected = await _userRepository.DeleteUserAsync(userId);

            return rowAffected>0;
        }

        public async Task<bool> IsUserExistAsync(int userId)
        {
            return await _userRepository.isUserExistAsync(userId);

        }




    }
}
