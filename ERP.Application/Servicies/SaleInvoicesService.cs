using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Mappings;
using ERP.Contacts.Requests.SaleInvoices;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;

namespace ERP.Application.Servicies;

public class SaleInvoicesService : ISaleInvoicesService
{
    private readonly ISaleInvoicesRepository _repository;

    public SaleInvoicesService(ISaleInvoicesRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<SaleInvoiceResponseDTO>> GetAllSaleInvoicesAsync()
    {
        var invoices = await _repository.GetAllSaleInvoicesAsync();

        return invoices.Select(x => x.ToResponseDTO()).ToList();
    }

    public async Task<SaleInvoiceResponseDTO?> GetSaleInvoiceByIdAsync(int saleInvoiceId)
    {
        var invoice = await _repository.GetSaleInvoiceByIdAsync(saleInvoiceId);

        return invoice?.ToResponseDTO();
    }

    public async Task<int> AddSaleInvoiceAsync(CreateSaleInvoiceDTO dto, int userId)
    {
        var invoice = new SaleInvoice(
            0,
            dto.CustomerId,
            userId,
            DateTime.Now,
            0,
            (byte)InvoiceStatus.Draft);

        return await _repository.AddSaleInvoiceAsync(invoice);
    }

    public Task<bool> DeleteSaleInvoiceAsync(int saleInvoiceId)
        => _repository.DeleteSaleInvoiceAsync(saleInvoiceId);

    public Task<bool> ConfirmSaleInvoiceAsync(int saleInvoiceId)
        => _repository.ConfirmSaleInvoiceAsync(saleInvoiceId);

    public Task<bool> CancelSaleInvoiceAsync(int saleInvoiceId)
        => _repository.CancelSaleInvoiceAsync(saleInvoiceId);
}
