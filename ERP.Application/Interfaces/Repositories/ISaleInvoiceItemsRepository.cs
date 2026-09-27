using ERP.Domain.Entities;

namespace ERP.Application.Interfaces.Repositories;

public interface ISaleInvoiceItemsRepository
{
    Task<List<SaleInvoiceItem>> GetAllSaleInvoiceItemsAsync();
    Task<SaleInvoiceItem?> GetSaleInvoiceItemByIdAsync(int saleInvoiceItemId);
    Task<List<SaleInvoiceItem>> GetItemsBySaleInvoiceIdAsync(int saleInvoiceId);
    Task<int> AddSaleInvoiceItemAsync(SaleInvoiceItem item);
    Task<bool> EditSaleInvoiceItemAsync(SaleInvoiceItem item);
    Task<bool> DeleteSaleInvoiceItemAsync(int saleInvoiceItemId);
    Task<bool> IsSaleInvoiceItemExistAsync(int saleInvoiceItemId);
}
