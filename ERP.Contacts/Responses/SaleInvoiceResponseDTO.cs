namespace ERP.Contacts.Responses;

public sealed record SaleInvoiceResponseDTO
{
    public int SaleInvoiceId { get; set; }
    public int CustomerId { get; set; }
    public int CreatedByUserId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    public byte Status { get; set; }
}
