using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Common.Results;
using MediatR;


namespace Inventra.Application.Features.Auths.Logins
{
    public class LoginCommandHandler: IRequestHandler<LoginCommandRequest,Result<LoginCommandResponse>>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Result<LoginCommandResponse>>Handle(LoginCommandRequest request,CancellationToken cancellationToken)
        {
            return await _authService.LoginAsync(request);
        }
    }
}
