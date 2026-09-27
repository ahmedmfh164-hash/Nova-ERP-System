using ERP.Domain.Entities;

namespace ERP.Application.Interfaces.Repositories
{
    public interface ISaleReturnsRepository
    {
        Task<List<SaleReturn>> GetAllSaleReturnsAsync();
        Task<SaleReturn?> GetSaleReturnByIdAsync(int ReturnId);
        Task<List<SaleReturn>> GetSaleReturnsBySaleInvoiceIdAsync(int SaleInvoiceId);
        Task<int> AddSaleReturnAsync(SaleReturn Entity);
        Task<bool> EditSaleReturnAsync(SaleReturn Entity);
        Task<bool> DeleteSaleReturnAsync(int ReturnId);
    }
}
