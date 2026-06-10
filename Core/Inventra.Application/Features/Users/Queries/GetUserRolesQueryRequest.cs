using Inventra.Application.Common.Results;
using Inventra.Application.Features.Users.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Users.Queries
{
    public class GetUserRolesQueryRequest: IRequest<Result<GetUserRolesQueryResponse>>
    {
        public Guid UserId { get; set; }
    }
}
