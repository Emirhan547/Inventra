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
    }
}
