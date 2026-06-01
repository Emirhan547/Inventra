using Inventra.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Products.Commands
{
    public class CreateProductCommandRequest:IRequest<Result<CreateProductCommandResponse>>
    {
        public Guid CategoryId { get; set; }

        public string Name { get; set; }

        public string SKU { get; set; }

        public decimal UnitPrice { get; set; }

        public int MinimumStockLevel { get; set; }
    }
}
