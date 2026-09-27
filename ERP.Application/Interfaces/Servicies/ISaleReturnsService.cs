using ERP.Contacts.Requests.SaleReturns;
using ERP.Contacts.Responses;

namespace ERP.Application.Interfaces.Servicies
{
    public interface ISaleReturnsService
    {
        Task<List<SaleReturnResponseDTO>> GetAllSaleReturnsAsync();
        Task<SaleReturnResponseDTO?> GetSaleReturnByIdAsync(int ReturnId);
        Task<List<SaleReturnResponseDTO>> GetSaleReturnsBySaleInvoiceIdAsync(int SaleInvoiceId);
        Task<int> AddSaleReturnAsync(CreateSaleReturnDTO DTO);
        Task<bool> EditSaleReturnAsync(UpdateSaleReturnDTO DTO);
        Task<bool> DeleteSaleReturnAsync(int ReturnId);
    }
}
