namespace ERP.Domain.Entities
{
    public class SaleReturnItem
    {
        public int ReturnItemId { get; set; }
        public int ReturnId { get; set; }
        public int SaleInvoiceItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }

        public SaleReturnItem(int returnItemId, int returnId, int saleInvoiceItemId, int quantity, decimal price, decimal totalAmount)
        {
            ReturnItemId = returnItemId;
            ReturnId = returnId;
            SaleInvoiceItemId = saleInvoiceItemId;
            Quantity = quantity;
            Price = price;
            TotalAmount = totalAmount;
        }
    }
}
