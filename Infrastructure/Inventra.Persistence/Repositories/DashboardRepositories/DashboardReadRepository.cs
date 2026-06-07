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
            x => x.Status ==
                PurchaseOrderStatus.Pending,
            cancellationToken),
            RecentMovements =
    await _context.StockMovements
        .Include(x => x.Stock)
            .ThenInclude(x => x.Product)
        .Include(x => x.Stock)
            .ThenInclude(x => x.Warehouse)
        .OrderByDescending(x => x.CreatedDate)
        .Take(5)
        .Select(x => new DashboardRecentMovementResponse
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
        .ToListAsync(cancellationToken),
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
        .ToListAsync(cancellationToken),

        };
    }
}