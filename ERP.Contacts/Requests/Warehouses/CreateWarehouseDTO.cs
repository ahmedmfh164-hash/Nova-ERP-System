namespace ERP.Contacts.Requests.Warehouses
{
    public sealed record CreateWarehouseDTO
    {
        public string? WarehouseName { get; set; }

        public string? Location { get; set; }
    }
}