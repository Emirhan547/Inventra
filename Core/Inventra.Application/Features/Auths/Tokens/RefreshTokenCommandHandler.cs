using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Auths.Logins;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Auths.Tokens
{
    public class RefreshTokenCommandHandler: IRequestHandler<RefreshTokenCommandRequest,Result<LoginCommandResponse>>
    {
        private readonly IAuthService _authService;

        public RefreshTokenCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<Result<LoginCommandResponse>>Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            return await _authService.RefreshTokenAsync(request);
        }
    }
}
