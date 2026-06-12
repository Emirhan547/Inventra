using Inventra.Application.Abstractions.Repositories.AuditLogRepositories;
using Inventra.Application.Abstractions.Repositories.CategoryRepositories;
using Inventra.Application.Abstractions.Repositories.DashboardRepositories;
using Inventra.Application.Abstractions.Repositories.ProductRepositories;
using Inventra.Application.Abstractions.Repositories.PurchaseOrderRepositories;
using Inventra.Application.Abstractions.Repositories.StockMovementRepositories;
using Inventra.Application.Abstractions.Repositories.StockRepositories;
using Inventra.Application.Abstractions.Repositories.SupplierRepositories;
using Inventra.Application.Abstractions.Repositories.WarehouseRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Persistence.Context;
using Inventra.Persistence.Interceptors;
using Inventra.Persistence.Repositories.AuditLogRepositories;
using Inventra.Persistence.Repositories.CategoryRepositories;
using Inventra.Persistence.Repositories.DashboardRepositories;
using Inventra.Persistence.Repositories.ProductRepositories;
using Inventra.Persistence.Repositories.PurchaseOrderRepositories;
using Inventra.Persistence.Repositories.StockMovementRepositories;
using Inventra.Persistence.Repositories.StockRepositories;
using Inventra.Persistence.Repositories.SupplierRepositories;
using Inventra.Persistence.Repositories.WarehouseRepositories;
using Inventra.Persistence.Uow;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Inventra.Persistence.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<AuditInterceptor>();

            services.AddDbContext<InventraDbContext>((sp, options) =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
                    options.AddInterceptors(sp.GetRequiredService<AuditInterceptor>());
                });

            #region Repositories

            services.AddScoped<IProductReadRepository,ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IWarehouseReadRepository,WarehouseReadRepository>();
            services.AddScoped<IWarehouseWriteRepository,WarehouseWriteRepository>();
            services.AddScoped<IStockMovementReadRepository, StockMovementReadRepository>();
            services.AddScoped<IStockMovementWriteRepository, StockMovementWriteRepository>();
            services.AddScoped<IStockWriteRepository, StockWriteRepository>();
            services.AddScoped<IDashboardReadRepository, DashboardReadRepository>();
            services.AddScoped<IStockReadRepository, StockReadRepository>();
            services.AddScoped<ISupplierReadRepository, SupplierReadRepository>();
            services.AddScoped<ISupplierWriteRepository, SupplierWriteRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPurchaseOrderReadRepository,PurchaseOrderReadRepository>();
            services.AddScoped<
    IAuditLogReadRepository,
    AuditLogReadRepository>();

            services.AddScoped<
                IAuditLogWriteRepository,
                AuditLogWriteRepository>();
            services.AddScoped<IPurchaseOrderWriteRepository,PurchaseOrderWriteRepository>();
            services.AddScoped<ICategoryReadRepository,CategoryReadRepository>();
            services.AddScoped<ICategoryWriteRepository,CategoryWriteRepository>();
            #endregion

            return services;
        }
    }
}