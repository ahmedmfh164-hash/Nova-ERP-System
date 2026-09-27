using ERP.Contacts.Requests.PurchaseInvoiceItems;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;

namespace ERP.Contacts.Mappings
{
    public static class PurchaseInvoiceItemMapping
    {
        public static PurchaseInvoiceItem ToEntity(this CreatePurchaseInvoiceItemDTO dto)
        {
            return new PurchaseInvoiceItem(
                PurchaseInvoiceItemId: 0,
                PurchaseInvoiceId: dto.PurchaseInvoiceId,
                ProductId: dto.ProductId,
                Quantity: dto.Quantity,
                Cost: dto.Cost,
                TotalAmount: dto.Quantity * dto.Cost
            );
        }

        public static PurchaseInvoiceItem ToEntity(this UpdatePurchaseInvoiceItemDTO dto)
        {
            return new PurchaseInvoiceItem(
                PurchaseInvoiceItemId: dto.PurchaseInvoiceItemId,
                PurchaseInvoiceId: -1,
                ProductId: dto.ProductId,
                Quantity: dto.Quantity,
                Cost: dto.Cost,
                TotalAmount: dto.Quantity * dto.Cost
            );
        }

        public static PurchaseInvoiceItemResponseDTO ToResponseDTO(this PurchaseInvoiceItem item)
        {
            return new PurchaseInvoiceItemResponseDTO
            {
                PurchaseInvoiceItemId = item.PurchaseInvoiceItemId,
                PurchaseInvoiceId = item.PurchaseInvoiceId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Cost = item.Cost,
                TotalAmount = item.TotalAmount
            };
        }
    }
}
