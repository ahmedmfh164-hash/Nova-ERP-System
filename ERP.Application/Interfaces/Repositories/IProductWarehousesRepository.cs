using ERP.Domain.Entities;

namespace ERP.Application.Interfaces.Repositories
{
    public interface IProductWarehousesRepository
    {
        Task<List<ProductWarehouse>> GetAllProductWarehousesAsync();
        Task<ProductWarehouse?> GetProductWarehouseAsync( int productId,int warehouseId);
        Task<List<ProductWarehouse>> GetProductInAllWarehousesAsync(int productId);
        Task<List<ProductWarehouse>> GetProductsInWarehouseAsync(int warehouseId);
        Task<bool> AddProductWarehouseAsync(ProductWarehouse productWarehouse);
        Task<bool> EditProductWarehouseAsync(ProductWarehouse productWarehouse);
        Task<bool> DeleteProductWarehouseAsync( int productId, int warehouseId);
        Task<bool> IsProductWarehouseExistAsync(int productId,int warehouseId);
    }
}