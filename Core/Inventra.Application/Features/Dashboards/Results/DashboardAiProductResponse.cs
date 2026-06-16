using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Dashboards.Results
{
    public sealed class DashboardAiProductResponse
    {
        public string ProductName { get; set; } = default!;

        public int CurrentStock { get; set; }

        public int MinimumStockLevel { get; set; }

        public int Last30DaysMovement { get; set; }
    }
}
