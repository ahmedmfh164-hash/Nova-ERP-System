using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services;
using ERP.Contacts.Mappings;
using ERP.Contacts.Requests.Pagination;
using ERP.Contacts.Requests.Products;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;

namespace ERP.Application.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsService(
            IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }


        public async Task<List<ProductResponseDTO>>GetAllProductsAsync(PaginationRequestDTO dto)
        {
            Pagination pagination=new ( dto.Page,dto.PageSize);
            var products =
                await _productsRepository.GetAllProductsAsync(pagination);

            return products .Select(product => product.ToResponseDTO()) .ToList();
        }


        public async Task<ProductResponseDTO?> GetProductByProductIdAsync(int productId)
        {
            var product = await _productsRepository.GetProductByProductIdAsync(productId);

            return product == null? null: product.ToResponseDTO();
        }


        public async Task<int?> AddProductAsync(CreateProductDTO dto)
        {
            var product = dto.ToEntity();
            int ProductId = await _productsRepository .AddProductAsync(product);

            return ProductId;
        }


        public async Task<bool> UpdateProductAsync(UpdateProductDTO dto)
        {
         
            return await _productsRepository
                .EditProductAsync(dto);
        }


        public async Task<bool>
            DeleteProductAsync(int productId)
        {
            return await _productsRepository
                .DeleteProductAsync(productId);
        }


        public async Task<bool>
            IsProductExistAsync(int productId)
        {
            return await _productsRepository
                .IsProductExistAsync(productId);
        }
    }
}