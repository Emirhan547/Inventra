using FluentValidation;
using Inventra.Application.Features.Products.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Products.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
            RuleFor(x => x.SKU).NotEmpty().MaximumLength(50);
            RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0);
            RuleFor(x => x.MinimumStockLevel).GreaterThanOrEqualTo(0);
            RuleFor(x => x.CategoryId).NotEmpty();
        }
    }
}
    
    

