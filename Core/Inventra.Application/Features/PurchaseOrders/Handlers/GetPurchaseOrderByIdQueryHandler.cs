using Inventra.Application.Abstractions.Repositories.PurchaseOrderRepositories;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.PurchaseOrders.Queries;
using Inventra.Application.Features.PurchaseOrders.Results;
using Mapster;
using MediatR;


namespace Inventra.Application.Features.PurchaseOrders.Handlers
{
    public class GetPurchaseOrderByIdQueryHandler(IPurchaseOrderReadRepository _repository): IRequestHandler<GetPurchaseOrderByIdQueryRequest,Result<GetPurchaseOrderByIdQueryResponse>>
    {
        public async Task<Result<GetPurchaseOrderByIdQueryResponse>>Handle(GetPurchaseOrderByIdQueryRequest request,CancellationToken cancellationToken)
        {
            var purchaseOrder =await _repository.GetDetailAsync(request.Id, cancellationToken);
            if (purchaseOrder is null)
            {
                return Result<GetPurchaseOrderByIdQueryResponse>.Failure("Purchase order not found.");
            }
            return Result<GetPurchaseOrderByIdQueryResponse>.SuccessResult(purchaseOrder.Adapt<GetPurchaseOrderByIdQueryResponse>());
        }
    }
}
