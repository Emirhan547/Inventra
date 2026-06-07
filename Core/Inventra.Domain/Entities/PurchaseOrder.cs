using Inventra.Domain.Common;
using Inventra.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Domain.Entities
{
    public class PurchaseOrder : BaseEntity
    {
        public string OrderNumber { get; set; }
        public Guid SupplierId { get; set; }

        public Supplier Supplier { get; set; }

        public PurchaseOrderStatus Status { get; set; }

        public decimal TotalAmount { get; set; }

        public List<PurchaseOrderItem> Item { get; set; } 
    }
}
