using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Mappings;
using ERP.Contacts.Requests.StockMovements;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;

namespace ERP.Application.Servicies;

public class StockMovementsService : IStockMovementsService
{
    private readonly IStockMovementsRepository _repository;

    public StockMovementsService(IStockMovementsRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<StockMovementResponseDTO>> GetAllStockMovementsAsync()
    {
        var movements = await _repository.GetAllStockMovementsAsync();

        return movements.Select(x => x.ToResponseDTO()).ToList();
    }

    public async Task<StockMovementResponseDTO?> GetStockMovementByIdAsync(int movementId)
    {
        var movement = await _repository.GetStockMovementByIdAsync(movementId);

        return movement?.ToResponseDTO();
    }

    public async Task<int> AddStockMovementAsync(CreateStockMovementDTO dto)
    {
        var movement = dto.ToEntity();

        return await _repository.AddStockMovementAsync(movement);
    }
}
