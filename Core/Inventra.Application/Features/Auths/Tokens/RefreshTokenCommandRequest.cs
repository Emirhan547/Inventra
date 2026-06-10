using Inventra.Application.Common.Results;
using Inventra.Application.Features.Auths.Logins;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Auths.Tokens
{
    public class RefreshTokenCommandRequest: IRequest<Result<LoginCommandResponse>>
    {
        public string RefreshToken { get; set; }
    }
}
