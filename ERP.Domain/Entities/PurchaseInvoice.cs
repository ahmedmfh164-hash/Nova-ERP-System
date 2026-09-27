namespace ERP.Domain.Entities
{
    public class PurchaseInvoice
    {
        public int PurchaseInvoiceId { get; set; }

        public int SupplierId { get; set; }

        public int CreatedByUserId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public decimal TotalAmount { get; set; }

        public byte Status { get; set; }

        public PurchaseInvoice(int PurchaseInvoiceId,int SupplierId,DateTime InvoiceDate,decimal TotalAmount,int CreatedByUserId,byte Status)
        {
            this.PurchaseInvoiceId = PurchaseInvoiceId;
            this.SupplierId = SupplierId;
            this.CreatedByUserId = CreatedByUserId;
            this.InvoiceDate = InvoiceDate;
            this.TotalAmount = TotalAmount;
            this.Status=Status;
        }
    }
}