namespace ERP.Contacts.Responses
{
    public sealed record SaleReturnItemResponseDTO
    {
        public int ReturnItemId { get; set; }
        public int ReturnId { get; set; }
        public int SaleInvoiceItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
