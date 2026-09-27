namespace ERP.Domain.Entities
{
    public class ProductWarehouse
    {
        public int ProductId { get; set; }

        public int WarehouseId { get; set; }

        public int Quantity { get; set; }

        public ProductWarehouse(int ProductId,int WarehouseId,int Quantity)
        {
            this.ProductId = ProductId;
            this.WarehouseId = WarehouseId;
            this.Quantity = Quantity;
        }
    }
}