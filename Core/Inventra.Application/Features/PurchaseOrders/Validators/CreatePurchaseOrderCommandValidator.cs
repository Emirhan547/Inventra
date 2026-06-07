using FluentValidation;
using Inventra.Application.Features.PurchaseOrders.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.PurchaseOrders.Validators
{
    public class CreatePurchaseOrderCommandValidator
     : AbstractValidator<CreatePurchaseOrderCommandRequest>
    {
        public CreatePurchaseOrderCommandValidator()
        {
            RuleFor(x => x.SupplierId)
                .NotEmpty();

            RuleFor(x => x.Items)
                .NotEmpty();
        }
    }
}