namespace ERP.Contacts.Requests.PurchaseInvoiceItems
{
    public sealed record UpdatePurchaseInvoiceItemDTO
    {
        public int PurchaseInvoiceItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
    }
}
