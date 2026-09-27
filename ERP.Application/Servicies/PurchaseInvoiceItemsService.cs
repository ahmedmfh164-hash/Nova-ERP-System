using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Mappings;
using ERP.Contacts.Requests.PurchaseInvoiceItems;
using ERP.Contacts.Responses;

namespace ERP.Application.Servicies
{
    public class PurchaseInvoiceItemsService : IPurchaseInvoiceItemsService
    {
        private readonly IPurchaseInvoiceItemsRepository _repository;

        public PurchaseInvoiceItemsService(IPurchaseInvoiceItemsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PurchaseInvoiceItemResponseDTO>> GetAllPurchaseInvoiceItemsAsync()
        {
            var items = await _repository.GetAllPurchaseInvoiceItemsAsync();
            return items.Select(item => item.ToResponseDTO()).ToList();
        }

        public async Task<PurchaseInvoiceItemResponseDTO?> GetPurchaseInvoiceItemByIdAsync(int purchaseInvoiceItemId)
        {
            var item = await _repository.GetPurchaseInvoiceItemByIdAsync(purchaseInvoiceItemId);
            return item == null ? null : item.ToResponseDTO();
        }

        public async Task<List<PurchaseInvoiceItemResponseDTO>> GetItemsByPurchaseInvoiceIdAsync(int purchaseInvoiceId)
        {
            var items = await _repository.GetItemsByPurchaseInvoiceIdAsync(purchaseInvoiceId);
            return items.Select(item => item.ToResponseDTO()).ToList();
        }

        public async Task<PurchaseInvoiceItemResponseDTO> AddPurchaseInvoiceItemAsync(CreatePurchaseInvoiceItemDTO dto)
        {
            var item = dto.ToEntity();
            item.PurchaseInvoiceItemId = await _repository.AddPurchaseInvoiceItemAsync(item);
            return item.ToResponseDTO();
        }

        public async Task<bool> UpdatePurchaseInvoiceItemAsync(UpdatePurchaseInvoiceItemDTO dto)
        {
            var item = dto.ToEntity();
            return await _repository.EditPurchaseInvoiceItemAsync(item);
        }

        public async Task<bool> DeletePurchaseInvoiceItemAsync(int purchaseInvoiceItemId)
        {
            return await _repository.DeletePurchaseInvoiceItemAsync(purchaseInvoiceItemId);
        }

        public async Task<bool> IsPurchaseInvoiceItemExistAsync(int purchaseInvoiceItemId)
        {
            return await _repository.IsPurchaseInvoiceItemExistAsync(purchaseInvoiceItemId);
        }
    }
}
