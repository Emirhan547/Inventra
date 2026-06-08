using Inventra.Application.Abstractions.Repositories.DashboardRepositories;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Dashboards.Queries;
using Inventra.Application.Features.Dashboards.Results;
using MediatR;

namespace Inventra.Application.Features.Dashboards.Handlers;

  public class GetDashboardQueryHandler: IRequestHandler<GetDashboardQueryRequest,Result<GetDashboardQueryResponse>>
{
    private readonly IDashboardReadRepository _repository;

    public GetDashboardQueryHandler(IDashboardReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetDashboardQueryResponse>>Handle(GetDashboardQueryRequest request,CancellationToken cancellationToken)
    {
        var dashboard =await _repository.GetDashboardAsync(cancellationToken);
        return Result<GetDashboardQueryResponse>.SuccessResult(dashboard);
    }
}