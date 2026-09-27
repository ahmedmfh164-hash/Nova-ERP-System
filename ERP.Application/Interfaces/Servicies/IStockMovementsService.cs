using ERP.Contacts.Requests.StockMovements;
using ERP.Contacts.Responses;

namespace ERP.Application.Interfaces.Servicies;

public interface IStockMovementsService
{
    Task<List<StockMovementResponseDTO>> GetAllStockMovementsAsync();
    Task<StockMovementResponseDTO?> GetStockMovementByIdAsync(int movementId);
    Task<int> AddStockMovementAsync(CreateStockMovementDTO dto);
}
