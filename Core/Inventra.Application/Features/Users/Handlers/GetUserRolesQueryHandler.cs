using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Users.Queries;
using Inventra.Application.Features.Users.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Users.Handlers
{
    public class GetUserRolesQueryHandler: IRequestHandler<GetUserRolesQueryRequest,Result<GetUserRolesQueryResponse>>
    {
        private readonly IUserService _userService;

        public GetUserRolesQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<GetUserRolesQueryResponse>>Handle(GetUserRolesQueryRequest request,CancellationToken cancellationToken)
        {
            return await _userService.GetUserRolesAsync(request.UserId);
        }
    }
}
