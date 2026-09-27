using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Mappings;
using ERP.Contacts.Requests.SaleInvoiceItems;
using ERP.Contacts.Responses;

namespace ERP.Application.Servicies;

public class SaleInvoiceItemsService : ISaleInvoiceItemsService
{
    private readonly ISaleInvoiceItemsRepository _repository;

    public SaleInvoiceItemsService(ISaleInvoiceItemsRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<SaleInvoiceItemResponseDTO>> GetAllSaleInvoiceItemsAsync()
    {
        var items = await _repository.GetAllSaleInvoiceItemsAsync();

        return items.Select(x => x.ToResponseDTO()).ToList();
    }

    public async Task<SaleInvoiceItemResponseDTO?> GetSaleInvoiceItemByIdAsync(int saleInvoiceItemId)
    {
        var item = await _repository.GetSaleInvoiceItemByIdAsync( saleInvoiceItemId);

        return item?.ToResponseDTO();
    }

    public async Task<List<SaleInvoiceItemResponseDTO>> GetItemsBySaleInvoiceIdAsync(int saleInvoiceId)
    {
        var items = await _repository.GetItemsBySaleInvoiceIdAsync(saleInvoiceId);

        return items.Select(x => x.ToResponseDTO()).ToList();
    }

    public async Task<int> AddSaleInvoiceItemAsync(CreateSaleInvoiceItemDTO dto)
    {
        var item = dto.ToEntity();

        return await _repository.AddSaleInvoiceItemAsync(item);
    }

    public async Task<bool> EditSaleInvoiceItemAsync(UpdateSaleInvoiceItemDTO dto)
    {
        var item = dto.ToEntity();

        return await _repository.EditSaleInvoiceItemAsync(item);
    }

    public Task<bool> DeleteSaleInvoiceItemAsync(int saleInvoiceItemId)
        => _repository.DeleteSaleInvoiceItemAsync(saleInvoiceItemId);
}
