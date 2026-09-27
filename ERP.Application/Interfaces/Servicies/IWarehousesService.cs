using ERP.Contacts.Requests.Warehouses;
using ERP.Contacts.Responses;

namespace ERP.Application.Interfaces.Servicies
{
    public interface IWarehousesService
    {
        Task<List<WarehouseResponseDTO>> GetAllWarehousesAsync();
        Task<WarehouseResponseDTO?> GetWarehouseByWarehouseIdAsync( int warehouseId);
        Task<WarehouseResponseDTO> AddWarehouseAsync( CreateWarehouseDTO dto);
        Task<bool> UpdateWarehouseAsync( UpdateWarehouseDTO dto);
        Task<bool> DeleteWarehouseAsync( int warehouseId);
        Task<bool> IsWarehouseExistAsync( int warehouseId);
    }
}