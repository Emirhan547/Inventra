using Inventra.Application.Abstractions.Repositories.GenericRepositories;
using Inventra.Application.Features.StockMovements.Results;
using Inventra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Abstractions.Repositories.StockMovementRepositories
{
    public interface IStockMovementReadRepository:IReadRepository<StockMovement>
    {
        Task<List<GetStockMovementsQueryResponse>>GetStockMovementsAsync(CancellationToken cancellationToken = default);
    }
}
