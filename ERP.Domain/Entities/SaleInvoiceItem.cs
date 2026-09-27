namespace ERP.Domain.Entities;

public class SaleInvoiceItem
{
    public int SaleInvoiceItemId { get; set; }
    public int SaleInvoiceId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalAmount { get; set; }

    public SaleInvoiceItem( int saleInvoiceItemId,int saleInvoiceId, int productId, int quantity, decimal price, decimal totalAmount)
    {
        SaleInvoiceItemId = saleInvoiceItemId;
        SaleInvoiceId = saleInvoiceId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
        TotalAmount = totalAmount;
    }
}
