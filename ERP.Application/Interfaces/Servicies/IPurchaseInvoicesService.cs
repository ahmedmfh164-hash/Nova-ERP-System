using ERP.Contacts.Requests.PurchaseInvoices;
using ERP.Contacts.Responses;

namespace ERP.Application.Interfaces.Servicies
{
    public interface IPurchaseInvoicesService
    {
        Task<List<PurchaseInvoiceResponseDTO>>GetAllPurchaseInvoicesAsync();
        Task<PurchaseInvoiceResponseDTO?>GetPurchaseInvoiceByIdAsync(int purchaseInvoiceId);
        Task<PurchaseInvoiceResponseDTO>AddPurchaseInvoiceAsync(int UserId,CreatePurchaseInvoiceDTO dto);
        Task<bool> ConfirmPurchaseInvoiceAsync(int id);
        Task<bool> CancelPurchaseInvoiceAsync(int id);
        Task<bool> IsPurchaseInvoiceExistAsync( int purchaseInvoiceId);
    }
}