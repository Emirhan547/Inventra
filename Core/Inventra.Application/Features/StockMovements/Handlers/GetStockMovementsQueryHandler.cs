using Inventra.Application.Abstractions.Repositories.StockMovementRepositories;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.StockMovements.Queries;
using Inventra.Application.Features.StockMovements.Results;
using MediatR;
namespace Inventra.Application.Features.StockMovements.Handlers
{
    public class GetStockMovementsQueryHandler
    : IRequestHandler<
        GetStockMovementsQuery,
        Result<
            List<GetStockMovementsQueryResponse>>>
    {
        private readonly
            IStockMovementReadRepository
            _repository;

        public GetStockMovementsQueryHandler(
            IStockMovementReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<GetStockMovementsQueryResponse>>>Handle(GetStockMovementsQuery request,CancellationToken cancellationToken)
        {
            var movements =await _repository.GetStockMovementsAsync(cancellationToken);
            return Result<List<GetStockMovementsQueryResponse>>.SuccessResult(movements);
        }
    }
}
