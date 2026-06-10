using Inventra.Domain.Constants;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Infrastructure.Extensions
{
    public static class AuthorizationRegistration
    {
        public static IServiceCollection
            AddInventraAuthorization(
                this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    Policies.DashboardAccess,
                    policy =>
                        policy.RequireRole(
                            Roles.Admin,
                            Roles.Manager,
                            Roles.Employee));

                options.AddPolicy(
                    Policies.ProductManagement,
                    policy =>
                        policy.RequireRole(
                            Roles.Admin,
                            Roles.Manager));

                options.AddPolicy(
                    Policies.CategoryManagement,
                    policy =>
                        policy.RequireRole(
                            Roles.Admin,
                            Roles.Manager));

                options.AddPolicy(
                    Policies.SupplierManagement,
                    policy =>
                        policy.RequireRole(
                            Roles.Admin,
                            Roles.Manager));

                options.AddPolicy(
                    Policies.WarehouseManagement,
                    policy =>
                        policy.RequireRole(
                            Roles.Admin,
                            Roles.Manager));

                options.AddPolicy(
                    Policies.StockOperation,
                    policy =>
                        policy.RequireRole(
                            Roles.Admin,
                            Roles.Manager,
                            Roles.Employee));

                options.AddPolicy(
                    Policies.PurchaseOrderCreate,
                    policy =>
                        policy.RequireRole(
                            Roles.Admin,
                            Roles.Manager,
                            Roles.Employee));

                options.AddPolicy(
                    Policies.PurchaseOrderApprove,
                    policy =>
                        policy.RequireRole(
                            Roles.Admin,
                            Roles.Manager));

                options.AddPolicy(
                    Policies.PurchaseOrderComplete,
                    policy =>
                        policy.RequireRole(
                            Roles.Admin,
                            Roles.Manager));

                options.AddPolicy(
                    Policies.UserManagement,
                    policy =>
                        policy.RequireRole(
                            Roles.Admin));
            });

            return services;
        }
    }
}