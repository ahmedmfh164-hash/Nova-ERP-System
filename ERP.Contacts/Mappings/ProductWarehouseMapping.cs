using ERP.Contacts.Requests.ProductWarehouses;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;

namespace ERP.Contacts.Mappings
{
    public static class ProductWarehouseMapping
    {
        public static ProductWarehouse ToEntity(this CreateProductWarehouseDTO dto)
        {
            return new ProductWarehouse(
                ProductId: dto.ProductId,
                WarehouseId: dto.WarehouseId,
                Quantity: dto.Quantity
            );
        }

        public static ProductWarehouse ToEntity( this UpdateProductWarehouseDTO dto)
        {
            return new ProductWarehouse(
                ProductId: dto.ProductId,
                WarehouseId: dto.WarehouseId,
                Quantity: dto.Quantity
            );
        }

        public static ProductWarehouseResponseDTO ToResponseDTO( this ProductWarehouse productWarehouse)
        {
            return new ProductWarehouseResponseDTO
            {
                ProductId = productWarehouse.ProductId,
                WarehouseId = productWarehouse.WarehouseId,
                Quantity = productWarehouse.Quantity
            };
        }
    }
}