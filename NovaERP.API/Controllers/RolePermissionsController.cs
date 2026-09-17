using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Requests.RolePermissions;
using ERP.Contacts.Requests.Roles;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;

namespace ERP.API.Controllers
{
    [Route("api/RolePermissions")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class RolePermissionsController : ControllerBase
    {
        private readonly IRolePermissionsService _service;

        public RolePermissionsController(IRolePermissionsService service)
        {
            _service= service;
        }


        [HttpGet("GetRolePermissions/{RoleId}", Name = "GetRolePermissionsAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetRolePermissionsAsync(int RoleId)
        {
            if (RoleId < 1)
                return BadRequest("Invalid Data");

            var rolePermissions = await _service.GetRolePermissionsByRoleIdAsync(RoleId);

            if (rolePermissions == null)
                return NotFound("Role has not any permissions.");

            return Ok(rolePermissions);
        }


        [HttpPost("AddRolePermission", Name = "AddRolePermissionAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddRolePermissionAsync([FromForm] RolePermissions rolePermissions)
        {
            if (rolePermissions==null||rolePermissions.RoleName==string.Empty||rolePermissions.Module==string.Empty
                ||rolePermissions.Action==string.Empty)
            {
                return BadRequest("Invalid Data");
            }

            var result = await _service.AddRolePermissionAsync(rolePermissions);

            if (result==0)
                return NotFound("This role has not any permissions.");

            return Ok($"Done added permissions successfully with Id : {result}");
        }


        [HttpDelete("Delete/{RoleId}", Name = "DeletePermissionsOfRoleAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeletePermissionsOfRoleAsync(int RoleId)
        {
            if (RoleId<1)
                return BadRequest("Invalid Id");

            if (await _service.DeletePermissionsOfRoleAsync(RoleId) != null)
                return Ok("Done delete permissions successfully.");
            else
                return NotFound("Role has not any permissions!");

        }


        [HttpDelete("Delete", Name = "DeleteOnePermissionsOfRoleAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteOnePermissionsOfRoleAsync(RolePermissionsRequestDTO drp)
        {
            if (drp.RoleId<1||drp.Module==string.Empty||drp.Action==string.Empty)
                return BadRequest("Invalid data");

            if (await _service.DeleteOnePermissionsOfRoleAsync(drp) != null)
                return Ok("Done delete permission successfully.");
            else
                return NotFound("Role has not any permissions!");

        }


        [HttpGet("exist/{RoleId}", Name = "IsRolePermissionsExistByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> IsRoleExistByIdAsync(int RoleId)
        {
            if (RoleId<1)
                return BadRequest("Invalid Id");

            bool isFound = await _service.IsRolePermissionsExistAsync(RoleId);

            if (!isFound)
                return NotFound("Role has not any permissions!");

            return Ok(isFound);
        }


        [HttpGet("existOne", Name = "IsOneRolePermissionsExistByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> IsOneRoleExistByIdAsync(RolePermissionsRequestDTO requestDTO)
        {
            if (requestDTO.RoleId<1||requestDTO.Module==string.Empty||requestDTO.Action==string.Empty)
                return BadRequest("Invalid data");

            bool isFound = await _service.IsRolePermissionsExistAsync(requestDTO);

            if (!isFound)
                return NotFound("Role has not any permissions!");

            return Ok(isFound);
        }


    }
}
