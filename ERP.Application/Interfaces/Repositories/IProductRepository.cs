using ERP.Contacts.Requests.Products;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;

namespace ERP.Application.Interfaces.Repositories
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAllProductsAsync(Pagination pagination);

        Task<Product?> GetProductByProductIdAsync(int productId);

        Task<int> AddProductAsync(Product product);

        Task<bool> EditProductAsync( UpdateProductDTO product);

        Task<bool> DeleteProductAsync( int productId);

        Task<bool> IsProductExistAsync( int productId);
    }
}