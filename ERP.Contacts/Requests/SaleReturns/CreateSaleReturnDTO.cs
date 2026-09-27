namespace ERP.Contacts.Requests.SaleReturns
{
    public sealed record CreateSaleReturnDTO
    {
        public int SaleInvoiceId { get; set; }
        public string ReasonReturn { get; set; }
    }
}
