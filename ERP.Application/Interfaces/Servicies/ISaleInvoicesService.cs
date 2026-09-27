using ERP.Contacts.Requests.SaleInvoices;
using ERP.Contacts.Responses;

namespace ERP.Application.Interfaces.Servicies;

public interface ISaleInvoicesService
{
    Task<List<SaleInvoiceResponseDTO>> GetAllSaleInvoicesAsync();
    Task<SaleInvoiceResponseDTO?> GetSaleInvoiceByIdAsync(int saleInvoiceId);
    Task<int> AddSaleInvoiceAsync(CreateSaleInvoiceDTO dto, int userId);
    Task<bool> DeleteSaleInvoiceAsync(int saleInvoiceId);
    Task<bool> ConfirmSaleInvoiceAsync(int saleInvoiceId);
    Task<bool> CancelSaleInvoiceAsync(int saleInvoiceId);
}
