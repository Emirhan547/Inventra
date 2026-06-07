using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Dashboards.Results
{
    public class DashboardCriticalStockResponse
    {
        public string ProductName { get; set; }

        public string WarehouseName { get; set; }

        public int Quantity { get; set; }

        public int MinimumStockLevel { get; set; }
    }
}
