using ERP.API.Authorization;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Requests.StockMovements;
using ERP.Contacts.Responses;
using ERP.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers;

[ApiController]
[Route("api/StockMovements")]
[Authorize]
public class StockMovementsController : ControllerBase
{
    private readonly IStockMovementsService _service;

    public StockMovementsController(IStockMovementsService service)
    {
        _service = service;
    }

    [HasPermission(PermissionModules.StockMovements, PermissionAction.Read)]
    [HttpGet("All", Name = "GetAllAsync")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<StockMovementResponseDTO>>> GetAllAsync()
    {
        var result = await _service.GetAllStockMovementsAsync();

        if (result.Count==0)
            return NotFound("Stock Movements not found.");

        return Ok(result);
    }

    [HasPermission(PermissionModules.StockMovements, PermissionAction.Read)]
    [HttpGet("GetById/{movementId}", Name = "GetByIdAsync")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetByIdAsync(int movementId)
    {
        var movement = await _service.GetStockMovementByIdAsync(movementId);

        if (movement is null)
            return NotFound("Stock Movement not found.");

        return Ok(movement);
    }

    [HasPermission(PermissionModules.StockMovements, PermissionAction.Create)]
    [HttpPost("Add", Name = "AddAsync")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> AddAsync([FromForm]CreateStockMovementDTO dto)
    {
        if (dto.ProductId<1)
            return BadRequest("Invalid Data");

        var id = await _service.AddStockMovementAsync(dto);

        if (id <= 0)
            return BadRequest("Failed to add sale return.");

        return CreatedAtRoute(nameof(GetByIdAsync), new { movementId = id }, $"Done added stock movement succesfully with id: {id}");
    }

}
