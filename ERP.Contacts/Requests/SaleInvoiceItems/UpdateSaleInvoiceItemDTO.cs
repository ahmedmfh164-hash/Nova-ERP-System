namespace ERP.Contacts.Requests.SaleInvoiceItems;

public sealed record UpdateSaleInvoiceItemDTO
{
    public int SaleInvoiceItemId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
