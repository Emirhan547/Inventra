using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Auths.Registers
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommandRequest>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(3).MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Şifreler eşleşmiyor.");
        }
    }
}