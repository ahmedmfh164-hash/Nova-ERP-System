using ERP.Contacts.Requests.Pagination;
using ERP.Contacts.Requests.Products;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;

namespace ERP.Application.Interfaces.Services
{
    public interface IProductsService
    {
        Task<List<ProductResponseDTO>> GetAllProductsAsync(PaginationRequestDTO dto);

        Task<ProductResponseDTO?> GetProductByProductIdAsync( int productId);

        Task<int?> AddProductAsync( CreateProductDTO dto);

        Task<bool> UpdateProductAsync( UpdateProductDTO dto);

        Task<bool> DeleteProductAsync( int productId);

        Task<bool> IsProductExistAsync( int productId);
    }
}