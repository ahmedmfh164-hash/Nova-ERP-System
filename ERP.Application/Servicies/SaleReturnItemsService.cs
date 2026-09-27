using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Mappings;
using ERP.Contacts.Requests.SaleReturns;
using ERP.Contacts.Responses;

namespace ERP.Application.Servicies
{
    public class SaleReturnItemsService : ISaleReturnItemsService
    {
        private readonly ISaleReturnItemsRepository _SaleReturnItemsRepository;

        public SaleReturnItemsService(ISaleReturnItemsRepository saleReturnItemsRepository)
        {
            _SaleReturnItemsRepository = saleReturnItemsRepository;
        }

        public async Task<List<SaleReturnItemResponseDTO>> GetAllSaleReturnItemsAsync()
        {
            var result = await _SaleReturnItemsRepository.GetAllSaleReturnItemsAsync();
            return result.Select(x => x.ToResponseDTO()).ToList();
        }

        public async Task<SaleReturnItemResponseDTO?> GetSaleReturnItemByIdAsync(int ReturnItemId)
        {
            var result = await _SaleReturnItemsRepository.GetSaleReturnItemByIdAsync(ReturnItemId);
            return result?.ToResponseDTO();
        }

        public async Task<List<SaleReturnItemResponseDTO>> GetItemsByReturnIdAsync(int ReturnId)
        {
            var result = await _SaleReturnItemsRepository.GetItemsByReturnIdAsync(ReturnId);
            return result.Select(x => x.ToResponseDTO()).ToList();
        }

        public async Task<int> AddSaleReturnItemAsync(CreateSaleReturnItemDTO DTO)
        {
            return await _SaleReturnItemsRepository.AddSaleReturnItemAsync(DTO.ToEntity());
        }

        public async Task<bool> EditSaleReturnItemAsync(UpdateSaleReturnItemDTO DTO)
        {
            var Existing = await _SaleReturnItemsRepository.GetSaleReturnItemByIdAsync(DTO.ReturnItemId);
            if (Existing == null)
                return false;

            Existing.Quantity = DTO.Quantity;
            return await _SaleReturnItemsRepository.EditSaleReturnItemAsync(Existing);
        }

        public async Task<bool> DeleteSaleReturnItemAsync(int ReturnItemId)
        {
            return await _SaleReturnItemsRepository.DeleteSaleReturnItemAsync(ReturnItemId);
        }
    }
}
