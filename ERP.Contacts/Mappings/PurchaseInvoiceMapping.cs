using ERP.Contacts.Requests.PurchaseInvoices;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;

namespace ERP.Contacts.Mappings
{
    public static class PurchaseInvoiceMapping
    {
        public static PurchaseInvoice ToEntity(this CreatePurchaseInvoiceDTO dto)
        {
            return new PurchaseInvoice(
                PurchaseInvoiceId: 0,
                SupplierId: dto.SupplierId,
                CreatedByUserId:-1,
                InvoiceDate:DateTime.Now,
                TotalAmount: 0,
                Status:(byte)InvoiceStatus.Draft
            );
        }

   
        public static PurchaseInvoiceResponseDTO ToResponseDTO(this PurchaseInvoice invoice)
        {
            return new PurchaseInvoiceResponseDTO
            {
                PurchaseInvoiceId = invoice.PurchaseInvoiceId,
                SupplierId = invoice.SupplierId,
                CreatedByUserId = invoice.CreatedByUserId,
                InvoiceDate = invoice.InvoiceDate,
                TotalAmount = invoice.TotalAmount,
                Status=invoice.Status
            };
        }
    }
}