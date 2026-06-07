using FluentValidation;
using Inventra.Application.Features.Categories.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Categories.Validators
{
    public class CreateCategoryCommandValidator:AbstractValidator<CreateCategoryCommandRequest>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x=> x.Name).NotEmpty().WithMessage("Category Boş Bırakılamaz");
        }
    }
}
