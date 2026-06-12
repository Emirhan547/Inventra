using Inventra.Infrastructure.Messaging.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Infrastructure.Extensions
{
    public static class MassTransitRegistration
    {
        public static IServiceCollection AddMassTransitServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<PurchaseOrderApprovedConsumer>();
                x.AddConsumer<LowStockDetectedConsumer>();
                x.AddConsumer<RoleAssignedAuditConsumer>();
                x.AddConsumer<PurchaseOrderApprovedAuditConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(
                        "localhost",
                        "/",
                        h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });

                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}