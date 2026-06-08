using Inventra.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Auths.Logins
{
    public class LoginCommandRequest: IRequest<
Result<LoginCommandResponse>>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
