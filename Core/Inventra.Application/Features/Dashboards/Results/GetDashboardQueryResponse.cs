using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Dashboards.Results
{
    public class GetDashboardQueryResponse
    {
        public int TotalProducts { get; set; }

        public int TotalWarehouses { get; set; }

        public int TotalStocks { get; set; }

        public int TotalStockMovements { get; set; }

        public int TotalSuppliers { get; set; }

        public int TotalPurchaseOrders { get; set; }

        public int PendingPurchaseOrders { get; set; }

        public List<DashboardRecentMovementResponse>
            RecentMovements
        { get; set; } = [];

        public List<DashboardCriticalStockResponse>
            CriticalStocks
        { get; set; } = [];
    }
}
