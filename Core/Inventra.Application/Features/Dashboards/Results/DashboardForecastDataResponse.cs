using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Dashboards.Results
{
    public sealed class DashboardForecastDataResponse
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = default!;

        public List<float> DailyConsumptions { get; set; }
            = [];
    }
}
