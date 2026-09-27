namespace ERP.Contacts.Requests.PurchaseInvoiceItems
{
    public sealed record CreatePurchaseInvoiceItemDTO
    {
        public int PurchaseInvoiceId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
    }
}
