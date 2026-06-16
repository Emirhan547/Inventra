using Inventra.Application.Abstractions.Infrastructures.AI;
using Inventra.Application.Abstractions.Repositories.DashboardRepositories;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Dashboards.Queries;
using Inventra.Application.Features.Dashboards.Results;
using MediatR;

namespace Inventra.Application.Features.Dashboards.Handlers;

public class GetDashboardQueryHandler
    : IRequestHandler<
        GetDashboardQueryRequest,
        Result<GetDashboardQueryResponse>>
{
    private readonly IDashboardReadRepository _repository;

    private readonly IAIService _aiService;

    public GetDashboardQueryHandler(
        IDashboardReadRepository repository,
        IAIService aiService)
    {
        _repository = repository;
        _aiService = aiService;
    }

    public async Task<Result<GetDashboardQueryResponse>>
        Handle(
            GetDashboardQueryRequest request,
            CancellationToken cancellationToken)
    {
        var dashboard =
            await _repository
                .GetDashboardAsync(
                    cancellationToken);

        var aiData =
            await _repository
                .GetAiAnalysisDataAsync(
                    cancellationToken);

        var prompt =
$"""
Aşağıdaki envanter verilerini analiz et.

Her ürün için:

- Stok yeterli mi?
- Satın alma gerekli mi?
- Öncelik seviyesi nedir?

Veriler:

{string.Join(
    Environment.NewLine,
    aiData.Select(x =>
        $"{x.ProductName} | " +
        $"Stok:{x.CurrentStock} | " +
        $"Min:{x.MinimumStockLevel} | " +
        $"30Gun:{x.Last30DaysMovement}"))}
""";

        var aiResult =
            await _aiService
                .GenerateAsync(
                    prompt,
                    cancellationToken);

        dashboard.AiPurchaseRecommendation =
            aiResult;

        return Result<GetDashboardQueryResponse>
            .SuccessResult(dashboard);
    }
}