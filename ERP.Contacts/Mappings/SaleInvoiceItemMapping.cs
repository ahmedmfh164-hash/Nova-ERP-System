using ERP.Contacts.Requests.SaleInvoiceItems;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;

namespace ERP.Contacts.Mappings;

public static class SaleInvoiceItemMapping
{
    public static SaleInvoiceItem ToEntity( this CreateSaleInvoiceItemDTO dto)
    {
        return new SaleInvoiceItem(
            0,
            dto.SaleInvoiceId,
            dto.ProductId,
            dto.Quantity,
            dto.Price,
            dto.Quantity * dto.Price);
    }

     public static SaleInvoiceItem ToEntity( this UpdateSaleInvoiceItemDTO dto)
    {
        return new SaleInvoiceItem(
            dto.SaleInvoiceItemId,
            0,
            0,
            dto.Quantity,
            dto.Price,
            dto.Quantity * dto.Price);
    }

    public static SaleInvoiceItemResponseDTO ToResponseDTO(this SaleInvoiceItem item)
    {
        return new SaleInvoiceItemResponseDTO
        {
            SaleInvoiceItemId = item.SaleInvoiceItemId,
            SaleInvoiceId = item.SaleInvoiceId,
            ProductId = item.ProductId,
            Quantity = item.Quantity,
            Price = item.Price,
            TotalAmount = item.TotalAmount
        };
    }
}
