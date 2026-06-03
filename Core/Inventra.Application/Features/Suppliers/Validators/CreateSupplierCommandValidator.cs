using FluentValidation;
using Inventra.Application.Features.Suppliers.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Suppliers.Validators
{
    public class CreateSupplierCommandValidator
    : AbstractValidator<CreateSupplierCommandRequest>
    {
        public CreateSupplierCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Phone)
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
