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
    public class GetUserByIdQueryHandler: IRequestHandler<GetUserByIdQueryRequest,Result<GetUserByIdQueryResponse>>
    {
        private readonly IUserService _userService;

        public GetUserByIdQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<GetUserByIdQueryResponse>>Handle(GetUserByIdQueryRequest request,CancellationToken cancellationToken)
        {
            return await _userService.GetUserByIdAsync(request.UserId);
        }
    }
}