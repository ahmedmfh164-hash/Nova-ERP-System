namespace ERP.Contacts.Requests.SaleReturns
{
    public sealed record UpdateSaleReturnItemDTO
    {
        public int ReturnItemId { get; set; }
        public int Quantity { get; set; }
    }
}
