using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Auths.Tokens
{
    public class RefreshTokenCommandValidator: AbstractValidator<RefreshTokenCommandRequest>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.RefreshToken).NotEmpty();
        }
    }
}
