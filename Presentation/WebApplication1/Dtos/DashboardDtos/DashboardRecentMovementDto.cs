namespace Inventra.WebUI.Dtos.DashboardDtos
{
    public class DashboardRecentMovementDto
    {
        public string ProductName { get; set; }

        public string WarehouseName { get; set; }

        public int Quantity { get; set; }

        public string Type { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
