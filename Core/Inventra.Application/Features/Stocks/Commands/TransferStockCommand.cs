using Inventra.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Stocks.Commands
{
    public class TransferStockCommand
     : IRequest<Result>
    {
        public Guid ProductId { get; set; }

        public Guid SourceWarehouseId { get; set; }

        public Guid DestinationWarehouseId { get; set; }

        public int Quantity { get; set; }

        public string? Description { get; set; }
    }
}
