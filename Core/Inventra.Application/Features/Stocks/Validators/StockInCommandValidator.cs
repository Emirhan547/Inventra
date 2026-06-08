using FluentValidation;
using Inventra.Application.Features.Stocks.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Stocks.Validators
{
    public class StockInCommandValidator
     : AbstractValidator<StockInCommand>
    {
        public StockInCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.WarehouseId).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}