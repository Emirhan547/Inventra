using Inventra.Application.Features.Dashboards.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Abstractions.Repositories.DashboardRepositories
{
    public interface IDashboardReadRepository
    {
        Task<GetDashboardQueryResponse>GetDashboardAsync(CancellationToken cancellationToken = default);
    }
}
