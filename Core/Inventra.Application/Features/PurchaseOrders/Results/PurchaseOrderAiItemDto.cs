using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.PurchaseOrders.Results
{
    public class PurchaseOrderAiItemDto
    {
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public int CurrentStock { get; set; }

        public int MinimumStockLevel { get; set; }

        public int Last30DaysMovement { get; set; }
    }
}
