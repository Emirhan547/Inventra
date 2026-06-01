using Inventra.Application.Abstractions.Repositories.ProductRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Persistence.Context;
using Inventra.Persistence.Interceptors;
using Inventra.Persistence.Repositories.ProductRepositories;
using Inventra.Persistence.Uow;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Persistence.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection
            AddPersistenceServices(
                this IServiceCollection services,
                IConfiguration configuration)
        {
            services.AddScoped<AuditInterceptor>();

            services.AddDbContext<InventraDbContext>(
                (sp, options) =>
                {
                    options.UseSqlServer(
                        configuration.GetConnectionString(
                            "SqlServer"));

                    options.AddInterceptors(
                        sp.GetRequiredService<
                            AuditInterceptor>());
                });

            #region Repositories

            services.AddScoped<
                IProductReadRepository,
                ProductReadRepository>();

            services.AddScoped<
                IProductWriteRepository,
                ProductWriteRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            return services;
        }
    }
}