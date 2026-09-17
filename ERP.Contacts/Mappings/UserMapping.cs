using ERP.Contacts.Requests.Users;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;

namespace ERP.Contacts.Mappings
{
    public static class UserMapping
    {
        public static User ToEntity(this CreatedUserDTO dto)
        {
            return new User(
                0,
               dto.UserName,
                dto.PersonId,
                dto.Password,
                dto.RoleName,
                dto.IsActive
                );
        }


     

        public static UserResponseDTO ToResponseDTO(this User user)
        {
            return new UserResponseDTO
            {
                UserId =user.UserId,
                PersonId = user.PersonId,
                UserName = user.UserName,
                PasswordHash = user.Password,
                RoleName = user.RoleName,
                IsActive = user.IsActive

            };
        }



    }
}
