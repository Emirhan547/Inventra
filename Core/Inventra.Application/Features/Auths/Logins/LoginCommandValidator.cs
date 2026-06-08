using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Auths.Logins
{
    public class LoginCommandValidator: AbstractValidator<LoginCommandRequest>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}