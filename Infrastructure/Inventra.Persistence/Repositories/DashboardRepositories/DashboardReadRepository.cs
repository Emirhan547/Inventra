using Inventra.Application.Abstractions.Repositories.DashboardRepositories;
using Inventra.Application.Features.Dashboards.Results;
using Inventra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Persistence.Repositories.DashboardRepositories;

public class DashboardReadRepository
    : IDashboardReadRepository
{
    private readonly InventraDbContext _context;

    public DashboardReadRepository(
        InventraDbContext context)
    {
        _context = context;
    }

    public async Task<GetDashboardQueryResponse>
        GetDashboardAsync(
            CancellationToken cancellationToken = default)
    {
        return new GetDashboardQueryResponse
        {
            TotalProducts =
                await _context.Products
                    .CountAsync(cancellationToken),

            TotalWarehouses =
                await _context.Warehouses
                    .CountAsync(cancellationToken),

            TotalStocks =
                await _context.Stocks
                    .CountAsync(cancellationToken),

            TotalStockMovements =
                await _context.StockMovements
                    .CountAsync(cancellationToken)
        };
    }
}