using ERP.Contacts.Requests.ProductWarehouses;
using ERP.Contacts.Responses;

namespace ERP.Application.Interfaces.Servicies
{
    public interface IProductWarehousesService
    {
        Task<List<ProductWarehouseResponseDTO>>GetAllProductWarehousesAsync();
        Task<ProductWarehouseResponseDTO?>GetProductWarehouseAsync(int productId,int warehouseId);
        Task<List<ProductWarehouseResponseDTO>> GetProductInAllWarehousesAsync(int productId);
        Task<List<ProductWarehouseResponseDTO>> GetProductsInWarehouseAsync(int warehouseId);
        Task<bool> AddProductWarehouseAsync(CreateProductWarehouseDTO dto);
        Task<bool> UpdateProductWarehouseAsync(UpdateProductWarehouseDTO dto);
        Task<bool> DeleteProductWarehouseAsync(int productId,int warehouseId);
        Task<bool> IsProductWarehouseExistAsync(int productId,int warehouseId);
    }
}