namespace ERP.Contacts.Requests.Warehouses
{
    public sealed record UpdateWarehouseDTO
    {
        public int WarehouseId { get; set; }

        public string? WarehouseName { get; set; }

        public string? Location { get; set; }
    }
}