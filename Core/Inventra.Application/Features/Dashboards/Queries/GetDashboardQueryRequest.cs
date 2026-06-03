using Inventra.Application.Common.Results;
using Inventra.Application.Features.Dashboards.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Dashboards.Queries
{
    public class GetDashboardQueryRequest
    : IRequest<Result<GetDashboardQueryResponse>>
    {
    }
}
