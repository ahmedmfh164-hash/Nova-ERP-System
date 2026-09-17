using ERP.Contacts.Requests.Users;
using ERP.Contacts.Responses;
                    
namespace ERP.Application.Interfaces.Servicies
{
    public interface IUserService
    {
        public Task<List<UserResponseDTO>> GetAllUsersAsync();
        public Task<int?> AddUserAsync(CreatedUserDTO dto);
        public Task<bool> EditUserInfoAsync(UpdatedUserDTO userDTO);
        public Task<UserResponseDTO?> GetUserByUserIdAsync(int userId);
        public Task<UserResponseDTO?> GetUserByEmailAsync(string email);
        public Task<bool> DeleteUserAsync(int userId);
        public Task<bool> IsUserExistAsync(int userId);



    }
}
