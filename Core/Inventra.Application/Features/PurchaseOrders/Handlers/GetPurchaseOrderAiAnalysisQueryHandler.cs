using Inventra.Application.Abstractions.Infrastructures.AI;
using Inventra.Application.Abstractions.Repositories.PurchaseOrderRepositories;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.PurchaseOrders.Queries;
using Inventra.Application.Features.PurchaseOrders.Results;
using MediatR;

namespace Inventra.Application.Features.PurchaseOrders.Handlers;

public sealed class GetPurchaseOrderAiAnalysisQueryHandler
    : IRequestHandler<
        GetPurchaseOrderAiAnalysisQueryRequest,
        Result<GetPurchaseOrderAiAnalysisQueryResponse>>
{
    private readonly IPurchaseOrderReadRepository _repository;

    private readonly IAIService _aiService;

    public GetPurchaseOrderAiAnalysisQueryHandler(
        IPurchaseOrderReadRepository repository,
        IAIService aiService)
    {
        _repository = repository;
        _aiService = aiService;
    }

    public async Task<Result<GetPurchaseOrderAiAnalysisQueryResponse>> Handle(
        GetPurchaseOrderAiAnalysisQueryRequest request,
        CancellationToken cancellationToken)
    {
        var purchaseOrder =
            await _repository.GetAiAnalysisDataAsync(
                request.Id,
                cancellationToken);

        if (purchaseOrder is null)
        {
            return Result<GetPurchaseOrderAiAnalysisQueryResponse>
                .Failure("Satın alma siparişi bulunamadı.");
        }

        var prompt =
$"""
Sen deneyimli bir Satın Alma Müdürü ve Envanter Uzmanısın.

Aşağıdaki satın alma siparişini analiz et.

Her ürün için aşağıdaki başlıklarda değerlendirme yap.

- Mevcut stok yeterli mi?
- Minimum stok seviyesine göre satın alma gerekli mi?
- Sipariş miktarı uygun mu?
- Risk seviyesi (Düşük / Orta / Yüksek)
- Satın alma önceliği
- Kısa gerekçe

En sonunda sipariş hakkında genel değerlendirme yap.

Cevabı Markdown formatında oluştur.

Tedarikçi:
{purchaseOrder.SupplierName}

Toplam Tutar:
{purchaseOrder.TotalAmount:C}

Sipariş Kalemleri:

{string.Join(
Environment.NewLine,
purchaseOrder.Items.Select(x =>
$"""
Ürün: {x.ProductName}
Sipariş Miktarı: {x.Quantity}
Birim Fiyat: {x.UnitPrice:C}
Mevcut Stok: {x.CurrentStock}
Minimum Stok: {x.MinimumStockLevel}
Son 30 Gün Hareketi: {x.Last30DaysMovement}
"""))}
""";

        var analysis =
            await _aiService.GenerateAsync(
                prompt,
                cancellationToken);

        return Result<GetPurchaseOrderAiAnalysisQueryResponse>
            .SuccessResult(
                new GetPurchaseOrderAiAnalysisQueryResponse
                {
                    Analysis = analysis
                });
    }
}