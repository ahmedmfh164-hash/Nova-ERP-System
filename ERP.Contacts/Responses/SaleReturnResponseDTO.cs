namespace ERP.Contacts.Responses
{
    public sealed record SaleReturnResponseDTO
    {
        public int ReturnId { get; set; }
        public int SaleInvoiceId { get; set; }
        public DateTime ReturnDate { get; set; }
        public string ReasonReturn { get; set; }
    }
}
