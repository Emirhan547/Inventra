using Inventra.Application.Abstractions.Repositories.GenericRepositories;
using Inventra.Application.Common.Pagination;
using Inventra.Application.Features.Stocks.Results;
using Inventra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Abstractions.Repositories.StockRepositories
{
    public interface IStockReadRepository:IReadRepository<Stock>
    {
        Task<Stock?> GetByProductAndWarehouseAsync(Guid productId,Guid warehouseId, bool tracking = true,CancellationToken cancellationToken = default);

        Task<PagedResponse<Stock>> GetPagedStocksAsync(int pageNumber,int pageSize,CancellationToken cancellationToken = default);
    }
}
