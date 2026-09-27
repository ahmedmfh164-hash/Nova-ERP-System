using ERP.Contacts.Requests.SaleReturns;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;

namespace ERP.Contacts.Mappings
{
    public static class SaleReturnMapping
    {
        public static SaleReturn ToEntity(this CreateSaleReturnDTO DTO)
        {
            return new SaleReturn(0, DTO.SaleInvoiceId, DateTime.Now, DTO.ReasonReturn);
        }

        public static SaleReturnResponseDTO ToResponseDTO(this SaleReturn Entity)
        {
            return new SaleReturnResponseDTO
            {
                ReturnId = Entity.ReturnId,
                SaleInvoiceId = Entity.SaleInvoiceId,
                ReturnDate = Entity.ReturnDate,
                ReasonReturn = Entity.ReasonReturn
            };
        }
    }
}
