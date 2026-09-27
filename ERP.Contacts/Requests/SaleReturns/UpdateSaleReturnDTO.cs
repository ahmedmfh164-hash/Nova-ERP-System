namespace ERP.Contacts.Requests.SaleReturns
{
    public sealed record UpdateSaleReturnDTO
    {
        public int ReturnId { get; set; }
        public string ReasonReturn { get; set; }
    }
}
