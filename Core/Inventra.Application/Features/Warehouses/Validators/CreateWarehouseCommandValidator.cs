using FluentValidation;
using Inventra.Application.Features.Warehouses.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Warehouses.Validators
{
    public class CreateWarehouseCommandValidator
    : AbstractValidator<CreateWarehouseCommandRequest>
    {
        public CreateWarehouseCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Location)
                .NotEmpty()
                .MaximumLength(250);
        }
    }
}
