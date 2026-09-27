using ERP.API.Authorization;
using ERP.Application.Interfaces.Services;
using ERP.Contacts.Requests.AuditLogs;
using ERP.Core.Enums ;
using ERP.Contacts.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ERP.API.Controllers
{
    [Route("api/AuditLogs")]
    [ApiController]
    public class AuditLogsController : ControllerBase
    {
        private readonly IAuditLogService _auditLogs;

        public AuditLogsController(IAuditLogService auditLogs)
        {
            _auditLogs = auditLogs;
        }

        [HttpGet("AllAuditLogs", Name = "GetAllAuditLogAsync")]
        [HasPermission(PermissionModules.AuditLogs, PermissionAction.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AuditLogResponseDTO>>> GetAllAuditLogAsync()
        {
            List<AuditLogResponseDTO> auditLogsList = await _auditLogs.GetAllAsync();

            if (auditLogsList.Count == 0)
                return NotFound("No audit logs found.");

            return Ok(auditLogsList);
        }

        [HttpGet("Get/{UserId}", Name = "GetByUserIdAsync")]
        [HasPermission(PermissionModules.AuditLogs, PermissionAction.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetByUserIdAsync(int UserId)
        {
            if (UserId < 1)
                return BadRequest("Invalid Data");

            List<AuditLogResponseDTO> auditLogsList = await _auditLogs.GetByUserIdAsync(UserId);

            if (auditLogsList.Count == 0)
                return NotFound("No audit logs found for this user.");

            return Ok(auditLogsList);
        }


        [HttpPost("AddAuditLog", Name = "AddAuditLogAsync")]
        [HasPermission(PermissionModules.AuditLogs, PermissionAction.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddAsync([FromForm] CreateAuditLogDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Action))
            {
                return BadRequest("Invalid Data");
            }

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _auditLogs.AddAsync(userId, dto);

            return StatusCode(StatusCodes.Status201Created, $"Done added audit log with id: {result}");
        }


    }
}
