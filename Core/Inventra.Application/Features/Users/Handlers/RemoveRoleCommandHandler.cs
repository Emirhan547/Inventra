using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Users.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Users.Handlers
{
    public class RemoveRoleCommandHandler: IRequestHandler<RemoveRoleCommandRequest,Result>
    {
        private readonly IUserService _userService;

        public RemoveRoleCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(RemoveRoleCommandRequest request,CancellationToken cancellationToken)
        {
            return await _userService.RemoveRoleAsync(request);
        }
    }
}