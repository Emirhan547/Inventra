using Inventra.Application.Features.PurchaseOrderItems.Results;
using Inventra.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.PurchaseOrders.Results
{
    public class GetPurchaseOrderByIdQueryResponse
    {
        public Guid Id { get; set; }

        public string OrderNumber { get; set; }

        public Guid SupplierId { get; set; }

        public string SupplierName { get; set; }

        public PurchaseOrderStatus Status { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<GetPurchaseOrderItemResponse> Items
        { get; set; } = [];
    }
}
