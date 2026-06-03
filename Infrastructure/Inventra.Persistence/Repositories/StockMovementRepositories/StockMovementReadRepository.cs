using Inventra.Application.Abstractions.Repositories.StockMovementRepositories;
using Inventra.Application.Features.StockMovements.Results;
using Inventra.Domain.Entities;
using Inventra.Persistence.Context;
using Inventra.Persistence.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Persistence.Repositories.StockMovementRepositories
{
    public class StockMovementReadRepository : ReadRepository<StockMovement>, IStockMovementReadRepository
    {
        public StockMovementReadRepository(InventraDbContext context) : base(context)
        {
        }
        public async Task<
    List<GetStockMovementsQueryResponse>>
    GetStockMovementsAsync(
        CancellationToken cancellationToken = default)
        {
            return await Table
                .AsNoTracking()
                .Select(x =>
                    new GetStockMovementsQueryResponse
                    {
                        Id = x.Id,

                        StockId = x.StockId,

                        ProductName =
                            x.Stock.Product.Name,

                        WarehouseName =
                            x.Stock.Warehouse.Name,

                        Type = x.Type,

                        Quantity = x.Quantity,

                        Description =
                            x.Description,

                        CreatedDate =
                            x.CreatedDate
                    })
                .OrderByDescending(
                    x => x.CreatedDate)
                .ToListAsync(
                    cancellationToken);
        }
    }
}
