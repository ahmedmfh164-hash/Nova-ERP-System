namespace ERP.Contacts.Responses
{
    public sealed record PurchaseInvoiceItemResponseDTO
    {
        public int PurchaseInvoiceItemId { get; set; }
        public int PurchaseInvoiceId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
