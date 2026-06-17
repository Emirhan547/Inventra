using Inventra.Application.Abstractions.Infrastructures.AI;
using Inventra.Application.Abstractions.Repositories.SupplierRepositories;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Suppliers.Queries;
using Inventra.Application.Features.Suppliers.Results;
using MediatR;
using System.Text.Json;

namespace Inventra.Application.Features.Suppliers.Handlers
{
    public sealed class GetSupplierAiAnalysisHandler
        : IRequestHandler<
            GetSupplierAiAnalysisQuery,
            Result<GetSupplierAiAnalysisResponse>>
    {
        private readonly ISupplierReadRepository _supplierReadRepository;
        private readonly IAIService _aiService;

        public GetSupplierAiAnalysisHandler(
            ISupplierReadRepository supplierReadRepository,
            IAIService aiService)
        {
            _supplierReadRepository = supplierReadRepository;
            _aiService = aiService;
        }

        public async Task<Result<GetSupplierAiAnalysisResponse>> Handle(
            GetSupplierAiAnalysisQuery request,
            CancellationToken cancellationToken)
        {
            var supplier =
                await _supplierReadRepository
                    .GetSupplierAiAnalysisDataAsync(
                        request.SupplierId,
                        cancellationToken);

            var prompt = $$"""
Sen deneyimli bir ERP satın alma uzmanısın.

Aşağıdaki tedarikçi verilerini analiz et.

SADECE JSON döndür.

JSON formatı:

{
  "score":0,
  "riskLevel":"",
  "summary":"",
  "strengths":[],
  "weaknesses":[],
  "recommendation":""
}

Veriler

Tedarikçi : {{supplier.SupplierName}}

Toplam Sipariş : {{supplier.TotalOrders}}

Tamamlanan : {{supplier.CompletedOrders}}

Bekleyen : {{supplier.PendingOrders}}

İptal : {{supplier.CancelledOrders}}

Toplam Tutar : {{supplier.TotalAmount}}

Ortalama Sipariş : {{supplier.AverageOrderAmount}}

Son Sipariş : {{supplier.LastOrderDate}}
""";

            var aiResult =
                await _aiService.GenerateAsync(
                    prompt,
                    cancellationToken);

            var response =
                JsonSerializer.Deserialize<GetSupplierAiAnalysisResponse>(
                    aiResult,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            if (response is null)
            {
                return Result<GetSupplierAiAnalysisResponse>
                    .Failure("AI analizi oluşturulamadı.");
            }

            return Result<GetSupplierAiAnalysisResponse>
                .SuccessResult(response);
        }
    }
}