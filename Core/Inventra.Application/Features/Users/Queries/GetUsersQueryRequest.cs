using Inventra.Application.Common.Pagination;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Users.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Users.Queries
{
    public sealed class GetUsersQueryRequest: PagedRequest,IRequest<Result<PagedResponse<GetUsersQueryResponse>>>
    {
        public string? Search { get; set; }
    }
}