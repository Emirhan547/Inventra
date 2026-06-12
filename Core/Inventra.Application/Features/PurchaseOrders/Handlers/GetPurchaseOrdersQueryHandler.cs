using Inventra.Application.Abstractions.Repositories.PurchaseOrderRepositories;
using Inventra.Application.Common.Pagination;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.PurchaseOrders.Queries;
using Inventra.Application.Features.PurchaseOrders.Results;
using Mapster;
using MediatR;

namespace Inventra.Application.Features.PurchaseOrders.Handlers
{
    public class GetPurchaseOrdersQueryHandler: IRequestHandler<GetPurchaseOrdersQueryRequest,Result<PagedResponse<GetPurchaseOrdersQueryResponse>>>
    {
        private readonly IPurchaseOrderReadRepository _repository;

        public GetPurchaseOrdersQueryHandler(IPurchaseOrderReadRepository repository)
        {
            _repository = repository;
        }

        public async Task< Result<PagedResponse<GetPurchaseOrdersQueryResponse>>>Handle( GetPurchaseOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            var pagedPurchaseOrders = await _repository.GetPagedAsync(request.PageNumber,request.PageSize,request.Status,request.SupplierId,cancellationToken);
            var response =new PagedResponse<GetPurchaseOrdersQueryResponse>
                {
                    Items =pagedPurchaseOrders.Items.Adapt<List<GetPurchaseOrdersQueryResponse>>(),
                    PageNumber =pagedPurchaseOrders.PageNumber,
                    PageSize =pagedPurchaseOrders.PageSize,
                    TotalCount =pagedPurchaseOrders.TotalCount,
                    TotalPages =pagedPurchaseOrders.TotalPages
                };

            return Result<PagedResponse< GetPurchaseOrdersQueryResponse>>.SuccessResult(response);
        }
    }
}