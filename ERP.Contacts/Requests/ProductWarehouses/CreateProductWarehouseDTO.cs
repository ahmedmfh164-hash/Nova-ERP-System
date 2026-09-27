namespace ERP.Contacts.Requests.ProductWarehouses
{
    public sealed record CreateProductWarehouseDTO
    {
        public int ProductId { get; set; }

        public int WarehouseId { get; set; }

        public int Quantity { get; set; }
    }
}