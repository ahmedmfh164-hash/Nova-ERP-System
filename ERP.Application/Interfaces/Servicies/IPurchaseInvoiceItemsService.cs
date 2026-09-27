using ERP.Contacts.Requests.PurchaseInvoiceItems;
using ERP.Contacts.Responses;

namespace ERP.Application.Interfaces.Servicies
{
    public interface IPurchaseInvoiceItemsService
    {
        Task<List<PurchaseInvoiceItemResponseDTO>> GetAllPurchaseInvoiceItemsAsync();
        Task<PurchaseInvoiceItemResponseDTO?> GetPurchaseInvoiceItemByIdAsync(int purchaseInvoiceItemId);
        Task<List<PurchaseInvoiceItemResponseDTO>> GetItemsByPurchaseInvoiceIdAsync(int purchaseInvoiceId);
        Task<PurchaseInvoiceItemResponseDTO> AddPurchaseInvoiceItemAsync(CreatePurchaseInvoiceItemDTO dto);
        Task<bool> UpdatePurchaseInvoiceItemAsync(UpdatePurchaseInvoiceItemDTO dto);
        Task<bool> DeletePurchaseInvoiceItemAsync(int purchaseInvoiceItemId);
        Task<bool> IsPurchaseInvoiceItemExistAsync(int purchaseInvoiceItemId);
    }
}
