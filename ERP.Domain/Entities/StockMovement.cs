namespace ERP.Domain.Entities;

public class StockMovement
{
    public int MovementId { get; set; }
    public int ProductId { get; set; }
    public byte MovementType { get; set; }
    public int Quantity { get; set; }
    public DateTime MovementDate { get; set; }
    public int ReferenceId { get; set; }

    public StockMovement(int movementId,int productId,byte movementType,int quantity,DateTime movementDate,int referenceId)
    {
        MovementId = movementId;
        ProductId = productId;
        MovementType = movementType;
        Quantity = quantity;
        MovementDate = movementDate;
        ReferenceId = referenceId;
    }
}
