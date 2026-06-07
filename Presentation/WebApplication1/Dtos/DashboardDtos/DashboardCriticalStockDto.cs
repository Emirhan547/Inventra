namespace Inventra.WebUI.Dtos.DashboardDtos
{
    public class DashboardCriticalStockDto
    {
        public string ProductName { get; set; }

        public string WarehouseName { get; set; }

        public int Quantity { get; set; }

        public int MinimumStockLevel { get; set; }
    }
}
