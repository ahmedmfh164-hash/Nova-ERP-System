using ERP.Domain.Entities;

namespace ERP.Application.Interfaces.Repositories;

public interface ISaleInvoicesRepository
{
    Task<List<SaleInvoice>> GetAllSaleInvoicesAsync();
    Task<SaleInvoice?> GetSaleInvoiceByIdAsync(int saleInvoiceId);
    Task<int> AddSaleInvoiceAsync(SaleInvoice invoice);
    Task<bool> DeleteSaleInvoiceAsync(int saleInvoiceId);
    Task<bool> IsSaleInvoiceExistAsync(int saleInvoiceId);
    Task<bool> ConfirmSaleInvoiceAsync(int saleInvoiceId);
    Task<bool> CancelSaleInvoiceAsync(int saleInvoiceId);
}
