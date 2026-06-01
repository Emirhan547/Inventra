using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Products.Results
{
    public class GetProductByIdQueryResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string SKU { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
