namespace Inventra.WebUI.Dtos.DashboardDtos
{
    public class DashboardSummaryDto
    {
        public int TotalProducts { get; set; }

        public int TotalWarehouses { get; set; }

        public int TotalStocks { get; set; }

        public int TotalStockMovements { get; set; }

        public int TotalSuppliers { get; set; }

        public int TotalPurchaseOrders { get; set; }

        public int PendingPurchaseOrders { get; set; }

        public List<DashboardRecentMovementDto>
            RecentMovements
        { get; set; } = [];

        public List<DashboardCriticalStockDto>
            CriticalStocks
        { get; set; } = [];
    }
}
