namespace ERP.Contacts.Responses;

public sealed record SaleInvoiceItemResponseDTO
{
    public int SaleInvoiceItemId { get; set; }
    public int SaleInvoiceId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalAmount { get; set; }
}
