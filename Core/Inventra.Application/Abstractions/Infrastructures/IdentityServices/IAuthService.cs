using Inventra.Application.Common.Results;
using Inventra.Application.Features.Auths.Logins;
using Inventra.Application.Features.Auths.Registers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Abstractions.Infrastructures.IdentityServices
{
    public interface IAuthService
    {
        Task<Result> RegisterAsync(RegisterCommandRequest request);

        Task<Result<LoginCommandResponse>>LoginAsync(LoginCommandRequest request);
    }
}
