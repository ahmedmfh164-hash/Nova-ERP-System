using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Mappings;
using ERP.Contacts.Requests.ProductWarehouses;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;

namespace ERP.Application.Servicies
{
    public class ProductWarehousesService
        : IProductWarehousesService
    {
        private readonly IProductWarehousesRepository
            _productWarehousesRepository;

        public ProductWarehousesService(
            IProductWarehousesRepository
                productWarehousesRepository)
        {
            _productWarehousesRepository =
                productWarehousesRepository;
        }

        public async Task<List<ProductWarehouseResponseDTO>> GetAllProductWarehousesAsync()
        {
            var list = await _productWarehousesRepository.GetAllProductWarehousesAsync();

            return list.Select(x => x.ToResponseDTO()).ToList();
        }

        public async Task<ProductWarehouseResponseDTO?>GetProductWarehouseAsync( int productId, int warehouseId)
        {
            var productWarehouse = await _productWarehousesRepository.GetProductWarehouseAsync(productId,warehouseId);

            return productWarehouse == null ? null: productWarehouse.ToResponseDTO();
        }

         public async Task<List<ProductWarehouseResponseDTO>>GetProductInAllWarehousesAsync( int productId)
        {
            var productWarehouse = await _productWarehousesRepository.GetProductInAllWarehousesAsync(productId);

            return productWarehouse.Select(x => x.ToResponseDTO()).ToList();
        }

        public async Task<List<ProductWarehouseResponseDTO>> GetProductsInWarehouseAsync(int warehouseId)
        {
            var productWarehouses = await _productWarehousesRepository.GetProductsInWarehouseAsync(warehouseId);

            return productWarehouses.Select(x => x.ToResponseDTO()).ToList();
        }

        public async Task<bool>AddProductWarehouseAsync(CreateProductWarehouseDTO dto)
        {
            var productWarehouse =dto.ToEntity();

            return await _productWarehousesRepository.AddProductWarehouseAsync( productWarehouse);
        }

        public async Task<bool>UpdateProductWarehouseAsync(UpdateProductWarehouseDTO dto)
        {
            var existing = await _productWarehousesRepository.GetProductWarehouseAsync( dto.ProductId, dto.WarehouseId);

            if (existing == null)
                return false;

            var productWarehouse =dto.ToEntity();

            return await _productWarehousesRepository.EditProductWarehouseAsync(productWarehouse);
        }

        public async Task<bool>DeleteProductWarehouseAsync( int productId, int warehouseId)
        {
            return await _productWarehousesRepository.DeleteProductWarehouseAsync(productId,warehouseId);
        }

        public async Task<bool>IsProductWarehouseExistAsync( int productId, int warehouseId)
        {
            return await _productWarehousesRepository.IsProductWarehouseExistAsync( productId,warehouseId);
        }
    }
}