using Inventra.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Stocks.Commands
{
    public class StockOutCommand : IRequest<Result>
    {
        public Guid ProductId { get; set; }

        public Guid WarehouseId { get; set; }

        public int Quantity { get; set; }

        public string? Description { get; set; }
    }
}
