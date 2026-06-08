using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Common.Results;
using MediatR;


namespace Inventra.Application.Features.Auths.Registers
{
    public class RegisterCommandHandler: IRequestHandler<RegisterCommandRequest,Result>
    {
        private readonly IAuthService _authService;

        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Result> Handle(RegisterCommandRequest request,
            CancellationToken cancellationToken)
        {
            return await _authService.RegisterAsync(request);
        }
    }
}
