using FluentValidation;
using Inventra.Application.Features.PurchaseOrderItems.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.PurchaseOrderItems.Validators
{
    public class CreatePurchaseOrderItemValidator
    : AbstractValidator<CreatePurchaseOrderItemRequest>
    {
        public CreatePurchaseOrderItemValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty();

            RuleFor(x => x.Quantity)
                .GreaterThan(0);

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0);
        }
    }
}
