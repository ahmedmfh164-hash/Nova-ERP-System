using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Mappings;
using ERP.Contacts.Requests.Warehouses;
using ERP.Contacts.Responses;

namespace ERP.Application.Servicies
{
    public class WarehousesService : IWarehousesService
    {
        private readonly IWarehousesRepository _warehousesRepository;

        public WarehousesService(
            IWarehousesRepository warehousesRepository)
        {
            _warehousesRepository = warehousesRepository;
        }

        public async Task<List<WarehouseResponseDTO>> GetAllWarehousesAsync()
        {
            var warehouses = await _warehousesRepository.GetAllWarehousesAsync();

            return warehouses.Select(warehouse => warehouse.ToResponseDTO()).ToList();
        }

        public async Task<WarehouseResponseDTO?> GetWarehouseByWarehouseIdAsync( int warehouseId)
        {
            var warehouse = await _warehousesRepository .GetWarehouseByWarehouseIdAsync( warehouseId);

            return warehouse == null ? null : warehouse.ToResponseDTO();
        }

        public async Task<WarehouseResponseDTO> AddWarehouseAsync( CreateWarehouseDTO dto)
        {
            var warehouse = dto.ToEntity();

            warehouse.WarehouseId = await _warehousesRepository .AddWarehouseAsync(warehouse);

            return warehouse.ToResponseDTO();
        }

        public async Task<bool> UpdateWarehouseAsync( UpdateWarehouseDTO dto)
        {
            var warehouse = dto.ToEntity();

            return await _warehousesRepository .EditWarehouseAsync(warehouse);
        }

        public async Task<bool> DeleteWarehouseAsync(int warehouseId)
        {
            return await _warehousesRepository .DeleteWarehouseAsync(warehouseId);
        }

        public async Task<bool> IsWarehouseExistAsync(int warehouseId)
        {
            return await _warehousesRepository.IsWarehouseExistAsync(warehouseId);
        }
    }
}