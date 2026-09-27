namespace ERP.Contacts.Responses
{
    public sealed record PurchaseInvoiceResponseDTO
    {
        public int PurchaseInvoiceId { get; set; }

        public int SupplierId { get; set; }

        public int CreatedByUserId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public decimal TotalAmount { get; set; }

        public byte Status { get; set; }
    }
}