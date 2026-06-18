using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.PurchaseOrders.Results
{
    public class PurchaseOrderAiDto
    {
        public string SupplierName { get; set; }

        public decimal TotalAmount { get; set; }

        public List<PurchaseOrderAiItemDto> Items { get; set; }
            = [];
    }
}
