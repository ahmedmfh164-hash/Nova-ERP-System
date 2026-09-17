using ERP.API.Authorization;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Requests.People;
using ERP.Contacts.Requests.Users;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ERP.Core.Enums;

namespace ERP.API.Controllers
{
    [Authorize]
    [Route("api/Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _user;

        public UserController(IUserService user)
        {
            _user = user;
        }


        [HasPermission(PermissionModules.Users, PermissionAction.Read)]
        [HttpGet("AllUsers", Name = "GetAllUsersAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetAllUsersAsync()
        {
            List<UserResponseDTO> userList = await _user.GetAllUsersAsync();

            if (userList.Count == 0)
                return NotFound("No users found.");

            return Ok(userList);
        }

        [HasPermission(PermissionModules.Users,PermissionAction.Read)]
        [Ownership]
        [HttpGet("GetUser/{UserId}", Name = "GetUserByUserIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetUserByUserIdAsync(int UserId)
        {
            if (UserId < 1)
                return BadRequest("Invalid Data");

            var user = await _user.GetUserByUserIdAsync(UserId);

            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddUser", Name = "AddUserAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddUserAsync([FromForm] CreatedUserDTO dto)
        {
            if (dto==null||dto.PersonId<1)
            {
                return BadRequest("Invalid Data");
            }

            var result = await _user.AddUserAsync(dto);

            if (result==0)
                return NotFound("This person is not found.");

            return Ok($"Done added user successfully with Id : {result}");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateUserInfo", Name = "UpdateUserInfoAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateUserInfoAsync([FromForm] UpdatedUserDTO updatedUser)
        {

            if (updatedUser == null||string.IsNullOrEmpty(updatedUser.UserName)||string.IsNullOrEmpty(updatedUser.RoleName))
            {
                return BadRequest("Invalid Data");
            }

            if (!await _user.IsUserExistAsync(updatedUser.UserId))
                return NotFound("This user is not found!");
       
            return await _user.EditUserInfoAsync(updatedUser) ? Ok("User updated successfully") : NotFound("User not found");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{userId}", Name = "DeleteUserAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteUserAsync(int userId)
        {
            if (userId<1)
                return BadRequest("Invalid Id");

            if (await _user.DeleteUserAsync(userId))
                return Ok("Done delete user successfully.");
            else
                return NotFound("This user is not found!");

        }
       
        [Authorize(Roles = "Admin")]
        [HttpGet("exist/{userId}", Name = "IsUserExistByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> IsUserExistByIdAsync(int userId)
        {
            bool isFound = await _user.IsUserExistAsync(userId);

            if (!isFound)
                return NotFound("Not Found");

            return Ok(isFound);
        }



    }
}
