using Inventra.Application.Features.Auths.Logins;
using Inventra.Application.Features.Auths.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Abstractions.Infrastructures.JwtServices
{
    public interface ITokenService
    {
       LoginCommandResponse CreateToken(TokenUser user);
    }
}
