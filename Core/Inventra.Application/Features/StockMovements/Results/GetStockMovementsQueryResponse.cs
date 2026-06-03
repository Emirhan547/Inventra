using Inventra.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.StockMovements.Results
{
    public class GetStockMovementsQueryResponse
    {
        public Guid Id { get; set; }

        public Guid StockId { get; set; }

        public string ProductName { get; set; }

        public string WarehouseName { get; set; }

        public StockMovementType Type { get; set; }

        public int Quantity { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
