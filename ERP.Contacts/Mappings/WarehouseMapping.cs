using ERP.Contacts.Requests.Warehouses;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;

namespace ERP.Contacts.Mappings
{
    public static class WarehouseMapping
    {
        public static Warehouse ToEntity( this CreateWarehouseDTO dto)
        {
            return new Warehouse(
                WarehouseId: 0,
                WarehouseName: dto.WarehouseName,
                Location: dto.Location
            );
        }

        public static Warehouse ToEntity(this UpdateWarehouseDTO dto)
        {
            return new Warehouse(
                WarehouseId: dto.WarehouseId,
                WarehouseName: dto.WarehouseName,
                Location: dto.Location
            );
        }

        public static WarehouseResponseDTO ToResponseDTO( this Warehouse warehouse)
        {
            return new WarehouseResponseDTO
            {
                WarehouseId = warehouse.WarehouseId,
                WarehouseName = warehouse.WarehouseName,
                Location = warehouse.Location
            };
        }
    }
}