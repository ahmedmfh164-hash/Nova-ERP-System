using ERP.Domain.Entities;

namespace ERP.Application.Interfaces.Repositories
{
    public interface IPurchaseInvoiceItemsRepository
    {
        Task<List<PurchaseInvoiceItem>> GetAllPurchaseInvoiceItemsAsync();
        Task<PurchaseInvoiceItem?> GetPurchaseInvoiceItemByIdAsync(int purchaseInvoiceItemId);
        Task<List<PurchaseInvoiceItem>> GetItemsByPurchaseInvoiceIdAsync(int purchaseInvoiceId);
        Task<int> AddPurchaseInvoiceItemAsync(PurchaseInvoiceItem item);
        Task<bool> EditPurchaseInvoiceItemAsync(PurchaseInvoiceItem item);
        Task<bool> DeletePurchaseInvoiceItemAsync(int purchaseInvoiceItemId);
        Task<bool> IsPurchaseInvoiceItemExistAsync(int purchaseInvoiceItemId);
    }
}
