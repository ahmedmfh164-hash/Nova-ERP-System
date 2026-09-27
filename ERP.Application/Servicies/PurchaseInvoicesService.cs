using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Mappings;
using ERP.Contacts.Requests.PurchaseInvoices;
using ERP.Contacts.Responses;

namespace ERP.Application.Servicies
{
    public class PurchaseInvoicesService
        : IPurchaseInvoicesService
    {
        private readonly IPurchaseInvoicesRepository
            _purchaseInvoicesRepository;

        public PurchaseInvoicesService( IPurchaseInvoicesRepository purchaseInvoicesRepository)
        {
            _purchaseInvoicesRepository =purchaseInvoicesRepository;
        }

        public async Task<List<PurchaseInvoiceResponseDTO>>GetAllPurchaseInvoicesAsync()
        {
            var invoices = await _purchaseInvoicesRepository.GetAllPurchaseInvoicesAsync();

            return invoices.Select(x => x.ToResponseDTO()).ToList();
        }

        public async Task<PurchaseInvoiceResponseDTO?>GetPurchaseInvoiceByIdAsync(int purchaseInvoiceId)
        {
            var invoice =await _purchaseInvoicesRepository.GetPurchaseInvoiceByIdAsync(purchaseInvoiceId);

            return invoice == null? null: invoice.ToResponseDTO();
        }


        public async Task<PurchaseInvoiceResponseDTO>AddPurchaseInvoiceAsync(int UserId,CreatePurchaseInvoiceDTO dto)
        {
            var invoice = dto.ToEntity();

            invoice.CreatedByUserId=UserId;

            invoice.PurchaseInvoiceId = await _purchaseInvoicesRepository.AddPurchaseInvoiceAsync(invoice);

            return invoice.ToResponseDTO();
        }

        public Task<bool> ConfirmPurchaseInvoiceAsync(int id) => _purchaseInvoicesRepository.ConfirmPurchaseInvoiceAsync(id);
        public Task<bool> CancelPurchaseInvoiceAsync(int id) => _purchaseInvoicesRepository.CancelPurchaseInvoiceAsync(id);


        public async Task<bool> IsPurchaseInvoiceExistAsync(int purchaseInvoiceId)
        {
            return await _purchaseInvoicesRepository.IsPurchaseInvoiceExistAsync( purchaseInvoiceId);
        }
    }
}