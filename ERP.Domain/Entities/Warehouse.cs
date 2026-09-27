namespace ERP.Domain.Entities
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }

        public string WarehouseName { get; set; }

        public string Location { get; set; }

        public Warehouse(int WarehouseId, string WarehouseName, string Location)
        {
            this.WarehouseId = WarehouseId;
            this.WarehouseName = WarehouseName;
            this.Location = Location;
        }
    }
}