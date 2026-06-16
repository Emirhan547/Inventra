using Inventra.Application.Abstractions.Repositories.DashboardRepositories;
using Inventra.Application.Features.Dashboards.Results;
using Inventra.Domain.Enums;
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

    public async Task<List<DashboardAiProductResponse>>
     GetAiAnalysisDataAsync(
         CancellationToken cancellationToken = default)
    {
        var thirtyDaysAgo =
            DateTime.UtcNow.AddDays(-30);

        return await _context.Stocks
            .Include(x => x.Product)
            .Select(x =>
                new DashboardAiProductResponse
                {
                    ProductName =
                        x.Product.Name,

                    CurrentStock =
                        x.Quantity,

                    MinimumStockLevel =
                        x.Product.MinimumStockLevel,

                    Last30DaysMovement =
                        _context.StockMovements
                            .Where(m =>
                                m.StockId == x.Id &&
                                m.CreatedDate >=
                                thirtyDaysAgo)
                            .Sum(m =>
                                (int?)m.Quantity) ?? 0
                })
            .ToListAsync(cancellationToken);
    }

    public async Task<GetDashboardQueryResponse>
        GetDashboardAsync(
            CancellationToken cancellationToken = default)
    {
        var movementData =
            await _context.StockMovements
                .Where(x =>
                    x.CreatedDate >=
                    DateTime.UtcNow.AddDays(-7))
                .ToListAsync(cancellationToken);

        var movementChart =
            movementData
                .GroupBy(x => x.CreatedDate.Date)
                .Select(g =>
                    new DashboardMovementChartResponse
                    {
                        Date =
                            g.Key.ToString("dd.MM"),

                        StockInCount =
                            g.Count(x =>
                                x.Type ==
                                StockMovementType.StockIn),

                        StockOutCount =
                            g.Count(x =>
                                x.Type ==
                                StockMovementType.StockOut)
                    })
                .OrderBy(x => x.Date)
                .ToList();

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
                    .CountAsync(cancellationToken),

            TotalSuppliers =
                await _context.Suppliers
                    .CountAsync(cancellationToken),

            TotalPurchaseOrders =
                await _context.PurchaseOrders
                    .CountAsync(cancellationToken),

            PendingPurchaseOrders =
                await _context.PurchaseOrders
                    .CountAsync(
                        x =>
                            x.Status ==
                            PurchaseOrderStatus.Pending,
                        cancellationToken),

            CriticalStockCount =
                await _context.Stocks
                    .Include(x => x.Product)
                    .CountAsync(
                        x =>
                            x.Quantity <=
                            x.Product.MinimumStockLevel,
                        cancellationToken),

            MovementChart =
                movementChart,

            RecentMovements =
                await _context.StockMovements
                    .Include(x => x.Stock)
                        .ThenInclude(x => x.Product)
                    .Include(x => x.Stock)
                        .ThenInclude(x => x.Warehouse)
                    .OrderByDescending(
                        x => x.CreatedDate)
                    .Take(5)
                    .Select(x =>
                        new DashboardRecentMovementResponse
                        {
                            ProductName =
                                x.Stock.Product.Name,

                            WarehouseName =
                                x.Stock.Warehouse.Name,

                            Quantity =
                                x.Quantity,

                            Type =
                                x.Type.ToString(),

                            CreatedDate =
                                x.CreatedDate
                        })
                    .ToListAsync(
                        cancellationToken),

            CriticalStocks =
                await _context.Stocks
                    .Include(x => x.Product)
                    .Include(x => x.Warehouse)
                    .Where(x =>
                        x.Quantity <=
                        x.Product.MinimumStockLevel)
                    .Select(x =>
                        new DashboardCriticalStockResponse
                        {
                            ProductName =
                                x.Product.Name,

                            WarehouseName =
                                x.Warehouse.Name,

                            Quantity =
                                x.Quantity,

                            MinimumStockLevel =
                                x.Product.MinimumStockLevel
                        })
                    .Take(5)
                    .ToListAsync(
                        cancellationToken)
        }
        ;


    }
}