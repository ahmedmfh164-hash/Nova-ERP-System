using ERP.Contacts.Requests.SaleReturns;
using ERP.Contacts.Responses;

namespace ERP.Application.Interfaces.Servicies
{
    public interface ISaleReturnItemsService
    {
        Task<List<SaleReturnItemResponseDTO>> GetAllSaleReturnItemsAsync();
        Task<SaleReturnItemResponseDTO?> GetSaleReturnItemByIdAsync(int ReturnItemId);
        Task<List<SaleReturnItemResponseDTO>> GetItemsByReturnIdAsync(int ReturnId);
        Task<int> AddSaleReturnItemAsync(CreateSaleReturnItemDTO DTO);
        Task<bool> EditSaleReturnItemAsync(UpdateSaleReturnItemDTO DTO);
        Task<bool> DeleteSaleReturnItemAsync(int ReturnItemId);
    }
}
