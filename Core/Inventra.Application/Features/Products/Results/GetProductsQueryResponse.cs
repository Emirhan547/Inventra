using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Products.Results
{
    public class GetProductsQueryResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string SKU { get; set; }

        public decimal UnitPrice { get; set; }

        public int MinimumStockLevel { get; set; }

        public string CategoryName { get; set; }
    }
}
