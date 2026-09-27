namespace ERP.Contacts.Responses
{
    public sealed record WarehouseResponseDTO
    {
        public int WarehouseId { get; set; }

        public string? WarehouseName { get; set; }

        public string? Location { get; set; }
    }
}