using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.PurchaseOrderItems.Results
{
    public class GetPurchaseOrderItemResponse
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
