using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Users.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Users.Handlers
{
    public class AssignRoleCommandHandler: IRequestHandler<AssignRoleCommandRequest,Result>
    {
        private readonly IUserService _userService;

        public AssignRoleCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result>Handle(AssignRoleCommandRequest request,CancellationToken cancellationToken)
        {
            return await _userService.AssignRoleAsync(request);
        }
    }
}