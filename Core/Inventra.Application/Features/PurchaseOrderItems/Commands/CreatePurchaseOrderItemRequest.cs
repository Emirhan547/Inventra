using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.PurchaseOrderItems.Commands
{
    public class CreatePurchaseOrderItemRequest
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
