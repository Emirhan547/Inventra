using Inventra.Application.Abstractions.Repositories.StockMovementRepositories;
using Inventra.Application.Common.Pagination;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.StockMovements.Queries;
using Inventra.Application.Features.StockMovements.Results;
using MediatR;

namespace Inventra.Application.Features.StockMovements.Handlers
{
    public class GetStockMovementsQueryHandler: IRequestHandler<GetStockMovementsQuery,Result<PagedResponse<GetStockMovementsQueryResponse>>>
    {
        private readonly IStockMovementReadRepository _repository;

        public GetStockMovementsQueryHandler( IStockMovementReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<PagedResponse<GetStockMovementsQueryResponse>>>Handle(GetStockMovementsQuery request, CancellationToken cancellationToken)
        {
            var pagedMovements =await _repository.GetPagedStockMovementsAsync(request.PageNumber,request.PageSize,request.Type,request.StartDate,request.EndDate,cancellationToken);
            var response = new PagedResponse<GetStockMovementsQueryResponse>
                {
                    Items = pagedMovements.Items.Select(x =>new GetStockMovementsQueryResponse
                            {
                                Id = x.Id,
                                StockId = x.StockId,
                                ProductName = x.Stock.Product.Name,
                                WarehouseName = x.Stock.Warehouse.Name,
                                Type = x.Type,
                                Quantity = x.Quantity,
                                Description = x.Description,
                                CreatedDate = x.CreatedDate
                            })
                        .ToList(),

                    PageNumber =pagedMovements.PageNumber,

                    PageSize =pagedMovements.PageSize,

                    TotalCount = pagedMovements.TotalCount,

                    TotalPages = pagedMovements.TotalPages
                };

            return Result<PagedResponse<GetStockMovementsQueryResponse>>.SuccessResult(response);
        }
    }
}