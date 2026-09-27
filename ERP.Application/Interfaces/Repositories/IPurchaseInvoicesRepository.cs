using ERP.Domain.Entities;

namespace ERP.Application.Interfaces.Repositories
{
    public interface IPurchaseInvoicesRepository
    {
        Task<List<PurchaseInvoice>> GetAllPurchaseInvoicesAsync();
        Task<PurchaseInvoice?> GetPurchaseInvoiceByIdAsync( int purchaseInvoiceId);
        Task<int> AddPurchaseInvoiceAsync( PurchaseInvoice purchaseInvoice);
        Task<bool> ConfirmPurchaseInvoiceAsync(int purchaseInvoiceId);
        Task<bool> CancelPurchaseInvoiceAsync(int purchaseInvoiceId);
        Task<bool> IsPurchaseInvoiceExistAsync( int purchaseInvoiceId);
    }
}