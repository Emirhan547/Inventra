using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Stocks.Results
{
    public class GetStocksQueryResponse
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public Guid WarehouseId { get; set; }

        public string WarehouseName { get; set; }

        public int Quantity { get; set; }
    }
}
