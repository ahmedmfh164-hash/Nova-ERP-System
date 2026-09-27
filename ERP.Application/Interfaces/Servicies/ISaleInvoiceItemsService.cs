using ERP.Contacts.Requests.SaleInvoiceItems;
using ERP.Contacts.Responses;

namespace ERP.Application.Interfaces.Servicies;

public interface ISaleInvoiceItemsService
{
    Task<List<SaleInvoiceItemResponseDTO>> GetAllSaleInvoiceItemsAsync();
    Task<SaleInvoiceItemResponseDTO?> GetSaleInvoiceItemByIdAsync(int saleInvoiceItemId);
    Task<List<SaleInvoiceItemResponseDTO>> GetItemsBySaleInvoiceIdAsync(int saleInvoiceId);
    Task<int> AddSaleInvoiceItemAsync(CreateSaleInvoiceItemDTO dto);
    Task<bool> EditSaleInvoiceItemAsync(UpdateSaleInvoiceItemDTO dto);
    Task<bool> DeleteSaleInvoiceItemAsync(int saleInvoiceItemId);
}
