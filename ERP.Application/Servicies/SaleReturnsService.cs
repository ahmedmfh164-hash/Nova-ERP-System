using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Mappings;
using ERP.Contacts.Requests.SaleReturns;
using ERP.Contacts.Responses;

namespace ERP.Application.Servicies
{
    public class SaleReturnsService : ISaleReturnsService
    {
        private readonly ISaleReturnsRepository _SaleReturnsRepository;

        public SaleReturnsService(ISaleReturnsRepository saleReturnsRepository)
        {
            _SaleReturnsRepository = saleReturnsRepository;
        }

        public async Task<List<SaleReturnResponseDTO>> GetAllSaleReturnsAsync()
        {
            var result = await _SaleReturnsRepository.GetAllSaleReturnsAsync();
            return result.Select(x => x.ToResponseDTO()).ToList();
        }

        public async Task<SaleReturnResponseDTO?> GetSaleReturnByIdAsync(int ReturnId)
        {
            var result = await _SaleReturnsRepository.GetSaleReturnByIdAsync(ReturnId);
            return result?.ToResponseDTO();
        }

        public async Task<List<SaleReturnResponseDTO>> GetSaleReturnsBySaleInvoiceIdAsync(int SaleInvoiceId)
        {
            var result = await _SaleReturnsRepository.GetSaleReturnsBySaleInvoiceIdAsync(SaleInvoiceId);
            return result.Select(x => x.ToResponseDTO()).ToList();
        }

        public async Task<int> AddSaleReturnAsync(CreateSaleReturnDTO DTO)
        {
            return await _SaleReturnsRepository.AddSaleReturnAsync(DTO.ToEntity());
        }

        public async Task<bool> EditSaleReturnAsync(UpdateSaleReturnDTO DTO)
        {
            var Existing = await _SaleReturnsRepository.GetSaleReturnByIdAsync(DTO.ReturnId);
            if (Existing == null)
                return false;

            Existing.ReasonReturn = DTO.ReasonReturn;
            return await _SaleReturnsRepository.EditSaleReturnAsync(Existing);
        }

        public async Task<bool> DeleteSaleReturnAsync(int ReturnId)
        {
            return await _SaleReturnsRepository.DeleteSaleReturnAsync(ReturnId);
        }
    }
}
