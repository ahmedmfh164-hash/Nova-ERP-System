namespace ERP.Domain.Entities;

public class SaleInvoice
{
    public int SaleInvoiceId { get; set; }
    public int CustomerId { get; set; }
    public int CreatedByUserId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    public byte Status { get; set; }

    public SaleInvoice( int saleInvoiceId,int customerId,int createdByUserId,DateTime invoiceDate, decimal totalAmount,byte status)
    {
        SaleInvoiceId = saleInvoiceId;
        CustomerId = customerId;
        CreatedByUserId = createdByUserId;
        InvoiceDate = invoiceDate;
        TotalAmount = totalAmount;
        Status = status;
    }
}
