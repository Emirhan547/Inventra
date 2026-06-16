using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Dashboards.Results
{
    public sealed class DashboardMovementChartResponse
    {
        public string Date { get; set; } = default!;

        public int StockInCount { get; set; }

        public int StockOutCount { get; set; }
    }
}
