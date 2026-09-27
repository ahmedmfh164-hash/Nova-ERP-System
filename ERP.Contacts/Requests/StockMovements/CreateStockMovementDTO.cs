using ERP.Core.Enums;

namespace ERP.Contacts.Requests.StockMovements;

public sealed record CreateStockMovementDTO
{
    public int ProductId { get; set; }
    public StockMovementType MovementType { get; set; }
    public int Quantity { get; set; }
    public int ReferenceId { get; set; }
}
