namespace ERP.Domain.Entities
{
    public class SaleReturn
    {
        public int ReturnId { get; set; }
        public int SaleInvoiceId { get; set; }
        public DateTime ReturnDate { get; set; }
        public string ReasonReturn { get; set; }

        public SaleReturn(int returnId, int saleInvoiceId, DateTime returnDate, string reasonReturn)
        {
            ReturnId = returnId;
            SaleInvoiceId = saleInvoiceId;
            ReturnDate = returnDate;
            ReasonReturn = reasonReturn;
        }
    }
}
