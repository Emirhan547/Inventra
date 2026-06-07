using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Dashboards.Results
{
    public class DashboardRecentMovementResponse
    {
        public string ProductName { get; set; }

        public string WarehouseName { get; set; }

        public int Quantity { get; set; }

        public string Type { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
