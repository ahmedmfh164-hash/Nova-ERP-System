namespace ERP.Domain.Entities
{
    public class PurchaseInvoiceItem
    {
        public int PurchaseInvoiceItemId { get; set; }
        public int PurchaseInvoiceId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal TotalAmount { get; set; }

        public PurchaseInvoiceItem( int PurchaseInvoiceItemId, int PurchaseInvoiceId, int ProductId, int Quantity, decimal Cost, decimal TotalAmount)
        {
            this.PurchaseInvoiceItemId = PurchaseInvoiceItemId;
            this.PurchaseInvoiceId = PurchaseInvoiceId;
            this.ProductId = ProductId;
            this.Quantity = Quantity;
            this.Cost = Cost;
            this.TotalAmount = TotalAmount;
        }
    }
}
