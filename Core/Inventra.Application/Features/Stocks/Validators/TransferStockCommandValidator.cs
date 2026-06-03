using FluentValidation;
using Inventra.Application.Features.Stocks.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Stocks.Validators
{
    public class TransferStockCommandValidator
     : AbstractValidator<TransferStockCommand>
    {
        public TransferStockCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty();

            RuleFor(x => x.SourceWarehouseId)
                .NotEmpty();

            RuleFor(x => x.DestinationWarehouseId)
                .NotEmpty();

            RuleFor(x => x.Quantity)
                .GreaterThan(0);

            RuleFor(x => x)
                .Must(x =>
                    x.SourceWarehouseId !=
                    x.DestinationWarehouseId)
                .WithMessage(
                    "Source and destination warehouses cannot be the same.");
        }
    }
}
