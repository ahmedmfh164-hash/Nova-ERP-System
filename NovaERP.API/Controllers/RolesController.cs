using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Requests.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
namespace ERP.API.Controllers
{
    [Route("api/Roles")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _role;

        public RolesController(IRoleService role)
        {
            _role=role;
        }


        [HttpGet("AllRoles", Name = "GetAllRolesAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Role>>> GetAllRolesAsync()
        {
            List<Role> roleList = await _role.GetAllRolesAsync();

            if (roleList.Count == 0)
                return NotFound("No Roles found.");

            return Ok(roleList);
        }


        [HttpGet("GetRole/{RoleId}", Name = "GetRoleByRoleIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetRoleByRoleIdAsync(int RoleId)
        {
            if (RoleId < 1)
                return BadRequest("Invalid Data");

            var role = await _role.GetRoleByRoleIdAsync(RoleId);

            if (role == null)
                return NotFound("Role not found");

            return Ok(role);
        }


        [HttpPost("AddRole", Name = "AddRoleAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddRoleAsync([FromForm] CreatedRoleDTO dto)
        {
            if (dto==null||dto.RoleName==string.Empty)
            {
                return BadRequest("Invalid Data");
            }

            var result = await _role.AddRoleAsync(dto);

            if (result==0)
                return NotFound("This role is not found.");

            return Ok($"Done added role successfully with Id : {result}");
        }


        [HttpDelete("Delete/{RoleId}", Name = "DeleteRoleAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteRoleAsync(int RoleId)
        {
            if (RoleId<1)
                return BadRequest("Invalid Id");

            if (await _role.DeleteRoleAsync(RoleId) != null)
                return Ok("Done delete role successfully.");
            else
                return NotFound("This role is not found!");

        }


        [HttpGet("exist/{RoleId}", Name = "IsRoleExistByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> IsRoleExistByIdAsync(int RoleId)
        {
            if (RoleId<1)
                return BadRequest("Invalid Id");

            bool isFound = await _role.IsRoleExistAsync(RoleId);

            if (!isFound)
                return NotFound("Not Found");

            return Ok(isFound);
        }





    }
}
