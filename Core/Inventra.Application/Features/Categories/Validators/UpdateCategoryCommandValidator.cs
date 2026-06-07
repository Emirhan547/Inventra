using FluentValidation;
using Inventra.Application.Features.Categories.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Categories.Validators
{
    public class UpdateCategoryCommandValidator:AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Category Boş Bırakılamaz");
        }
    }
}
