using ERP.Contacts.Requests.Products;
using ERP.Contacts.Requests.StockMovements;
using ERP.Contacts.Responses;
using ERP.Core.Enums;
using ERP.Domain.Entities;
using SMT = ERP.Core.Enums.StockMovementType;
namespace ERP.Contacts.Mappings;

public static class StockMovementMapping
{

    public static StockMovement ToEntity(this CreateStockMovementDTO dto)
    {
        return new StockMovement(
            0,
            dto.ProductId,
            (byte)dto.MovementType,
            dto.Quantity,
            DateTime.Now,
            dto.ReferenceId
            );
    }

    public static StockMovementResponseDTO ToResponseDTO( this StockMovement movement)
    {
        return new StockMovementResponseDTO
        {
            MovementId = movement.MovementId,
            ProductId = movement.ProductId,
            MovementType = movement.MovementType==(int)SMT.Purchase?SMT.Purchase.ToString():
            movement.MovementType==(int)SMT.Sale?SMT.Sale.ToString():SMT.SaleReturn.ToString(),
            Quantity = movement.Quantity,
            MovementDate = movement.MovementDate,
            ReferenceId = movement.ReferenceId
        };
    }
}
