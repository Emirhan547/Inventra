using Inventra.Application.Abstractions.Repositories.GenericRepositories;
using Inventra.Application.Common.Pagination;
using Inventra.Application.Features.StockMovements.Results;
using Inventra.Domain.Entities;
using Inventra.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Abstractions.Repositories.StockMovementRepositories
{
    public interface IStockMovementReadRepository:IReadRepository<StockMovement>
    {
        Task<PagedResponse<StockMovement>>GetPagedStockMovementsAsync(int pageNumber,int pageSize,StockMovementType? type,DateTime? startDate,DateTime? endDate,CancellationToken cancellationToken = default);
    }
}
