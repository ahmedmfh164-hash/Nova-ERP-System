using ERP.Domain.Entities;

namespace ERP.Application.Interfaces.Repositories
{
    public interface IWarehousesRepository
    {
        Task<List<Warehouse>> GetAllWarehousesAsync();
        Task<Warehouse?> GetWarehouseByWarehouseIdAsync( int warehouseId);
        Task<int> AddWarehouseAsync( Warehouse warehouse);
        Task<bool> EditWarehouseAsync( Warehouse warehouse);
        Task<bool> DeleteWarehouseAsync( int warehouseId);
        Task<bool> IsWarehouseExistAsync(int warehouseId);
    }
}