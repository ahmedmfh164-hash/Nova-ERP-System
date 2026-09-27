using ERP.Contacts.Requests.SaleReturns;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;

namespace ERP.Contacts.Mappings
{
    public static class SaleReturnItemMapping
    {
        public static SaleReturnItem ToEntity(this CreateSaleReturnItemDTO DTO)
        {
            return new SaleReturnItem(0, DTO.ReturnId, DTO.SaleInvoiceItemId, DTO.Quantity, 0, 0);
        }

        public static SaleReturnItemResponseDTO ToResponseDTO(this SaleReturnItem Entity)
        {
            return new SaleReturnItemResponseDTO
            {
                ReturnItemId = Entity.ReturnItemId,
                ReturnId = Entity.ReturnId,
                SaleInvoiceItemId = Entity.SaleInvoiceItemId,
                Quantity = Entity.Quantity,
                Price = Entity.Price,
                TotalAmount = Entity.TotalAmount
            };
        }
    }
}
