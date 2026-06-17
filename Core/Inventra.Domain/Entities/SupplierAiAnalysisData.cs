using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Domain.Entities
{
    public sealed class SupplierAiAnalysisData
    {
        public string SupplierName { get; set; } = string.Empty;

        public int TotalOrders { get; set; }

        public int CompletedOrders { get; set; }

        public int PendingOrders { get; set; }

        public int CancelledOrders { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal AverageOrderAmount { get; set; }

        public DateTime? LastOrderDate { get; set; }
    }
}
