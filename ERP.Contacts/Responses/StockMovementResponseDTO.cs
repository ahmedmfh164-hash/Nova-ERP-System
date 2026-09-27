namespace ERP.Contacts.Responses;

public sealed record StockMovementResponseDTO
{
    public int MovementId { get; set; }
    public int ProductId { get; set; }
    public string MovementType { get; set; }
    public int Quantity { get; set; }
    public DateTime MovementDate { get; set; }
    public int ReferenceId { get; set; }
}
