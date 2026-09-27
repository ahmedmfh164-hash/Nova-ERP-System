namespace ERP.Contacts.Responses
{
    public sealed record ProductWarehouseResponseDTO
    {
        public int ProductId { get; set; }

        public int WarehouseId { get; set; }

        public int Quantity { get; set; }
    }
}