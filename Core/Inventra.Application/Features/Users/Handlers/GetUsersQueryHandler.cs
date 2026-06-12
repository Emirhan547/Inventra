using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Common.Pagination;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Users.Queries;
using Inventra.Application.Features.Users.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Users.Handlers
{
    public class GetUsersQueryHandler: IRequestHandler<GetUsersQueryRequest,Result<PagedResponse<GetUsersQueryResponse>>>
    {
        private readonly IUserService _userService;

        public GetUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result< PagedResponse<GetUsersQueryResponse>>>Handle( GetUsersQueryRequest request,CancellationToken cancellationToken)
        {
            return await _userService.GetUsersAsync(request.PageNumber,request.PageSize,request.Search);
        }
    }
}
