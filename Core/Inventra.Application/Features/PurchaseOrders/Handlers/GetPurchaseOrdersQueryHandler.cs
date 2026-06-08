using Inventra.Application.Abstractions.Repositories.PurchaseOrderRepositories;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.PurchaseOrders.Queries;
using Inventra.Application.Features.PurchaseOrders.Results;
using Mapster;
using MediatR;


namespace Inventra.Application.Features.PurchaseOrders.Handlers
{
    public class GetPurchaseOrdersQueryHandler(IPurchaseOrderReadRepository _repository): IRequestHandler<GetPurchaseOrdersQueryRequest,Result<List<GetPurchaseOrdersQueryResponse>>>
    {
        public async Task<Result<List<GetPurchaseOrdersQueryResponse>>>Handle(GetPurchaseOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            var purchaseOrders =await _repository.GetAllAsync(cancellationToken:cancellationToken);
            return Result<List<GetPurchaseOrdersQueryResponse>>.SuccessResult( purchaseOrders.Adapt<List<GetPurchaseOrdersQueryResponse>>());
        }
    }
}
