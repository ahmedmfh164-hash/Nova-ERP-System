namespace ERP.Contacts.Requests.SaleInvoiceItems;

public sealed record CreateSaleInvoiceItemDTO
{
    public int SaleInvoiceId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
