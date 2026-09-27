using ERP.Contacts.Responses;
using ERP.Domain.Entities;

namespace ERP.Contacts.Mappings;

public static class SaleInvoiceMapping
{
    public static SaleInvoiceResponseDTO ToResponseDTO( this SaleInvoice invoice)
    {
        return new SaleInvoiceResponseDTO
        {
            SaleInvoiceId = invoice.SaleInvoiceId,
            CustomerId = invoice.CustomerId,
            CreatedByUserId = invoice.CreatedByUserId,
            InvoiceDate = invoice.InvoiceDate,
            TotalAmount = invoice.TotalAmount,
            Status = invoice.Status
        };
    }
}
