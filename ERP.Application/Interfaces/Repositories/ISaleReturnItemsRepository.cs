using ERP.Domain.Entities;

namespace ERP.Application.Interfaces.Repositories
{
    public interface ISaleReturnItemsRepository
    {
        Task<List<SaleReturnItem>> GetAllSaleReturnItemsAsync();
        Task<SaleReturnItem?> GetSaleReturnItemByIdAsync(int ReturnItemId);
        Task<List<SaleReturnItem>> GetItemsByReturnIdAsync(int ReturnId);
        Task<int> AddSaleReturnItemAsync(SaleReturnItem Entity);
        Task<bool> EditSaleReturnItemAsync(SaleReturnItem Entity);
        Task<bool> DeleteSaleReturnItemAsync(int ReturnItemId);
    }
}
