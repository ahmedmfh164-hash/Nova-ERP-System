using ERP.Domain.Entities;

namespace ERP.Application.Interfaces.Repositories;

public interface IStockMovementsRepository
{
    Task<List<StockMovement>> GetAllStockMovementsAsync();
    Task<StockMovement?> GetStockMovementByIdAsync(int movementId);
    Task<int> AddStockMovementAsync(StockMovement movement);
}
