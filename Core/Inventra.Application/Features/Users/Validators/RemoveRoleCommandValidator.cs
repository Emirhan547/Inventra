using FluentValidation;
using Inventra.Application.Features.Users.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Users.Validators
{
    public class RemoveRoleCommandValidator: AbstractValidator<RemoveRoleCommandRequest>
    {
        public RemoveRoleCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.RoleName).NotEmpty();
        }
    }
}