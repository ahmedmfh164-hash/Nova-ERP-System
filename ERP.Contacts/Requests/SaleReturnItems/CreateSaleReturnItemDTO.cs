namespace ERP.Contacts.Requests.SaleReturns
{
    public sealed record CreateSaleReturnItemDTO
    {
        public int ReturnId { get; set; }
        public int SaleInvoiceItemId { get; set; }
        public int Quantity { get; set; }
    }
}
