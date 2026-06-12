using Inventra.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Inventra.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Inventra.Persistence.Context
{
    public class InventraDbContext :IdentityDbContext<AppUser,AppRole,Guid>
    {
        public InventraDbContext(DbContextOptions<InventraDbContext> options): base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Warehouse> Warehouses => Set<Warehouse>();
        public DbSet<Stock> Stocks => Set<Stock>();
        public DbSet<StockMovement> StockMovements=> Set<StockMovement>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
        public DbSet<PurchaseOrderItem> PurchaseOrderItems
            => Set<PurchaseOrderItem>();
        public DbSet<AuditLog> AuditLogs { get; set; }
        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(InventraDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
