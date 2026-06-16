using Inventra.Domain.Entities;
using Inventra.Domain.Enums;
using Inventra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Persistence.Seed;

public static class DemoDataGenerator
{
    public static async Task GenerateStockMovementsAsync(
        InventraDbContext context)
    {
        if (await context.StockMovements.AnyAsync())
        {
            return;
        }

        var stocks =
            await context.Stocks
                .Include(x => x.Product)
                .ToListAsync();

        var random =
            new Random();

        foreach (var stock in stocks)
        {
            for (int day = 60; day >= 1; day--)
            {
                int quantity;

                if (stock.Product.Name.Contains(
                        "Mouse",
                        StringComparison.OrdinalIgnoreCase))
                {
                    quantity =
                        random.Next(4, 8);
                }
                else if (
                    stock.Product.Name.Contains(
                        "Klavye",
                        StringComparison.OrdinalIgnoreCase))
                {
                    quantity =
                        random.Next(2, 5);
                }
                else
                {
                    quantity =
                        random.Next(0, 2);
                }

                context.StockMovements.Add(
                    new StockMovement
                    {
                        Id = Guid.NewGuid(),

                        StockId =
                            stock.Id,

                        Type =
                            StockMovementType.StockOut,

                        Quantity =
                            quantity,

                        Description =
                            "ML.NET Demo Data",

                        CreatedDate =
                            DateTime.UtcNow
                                .AddDays(-day)
                    });
            }
        }

        await context.SaveChangesAsync();
    }
}