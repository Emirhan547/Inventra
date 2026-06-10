using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Domain.Constants
{
    public static class Policies
    {
        public const string DashboardAccess =
            nameof(DashboardAccess);

        public const string ProductManagement =
            nameof(ProductManagement);

        public const string CategoryManagement =
            nameof(CategoryManagement);

        public const string SupplierManagement =
            nameof(SupplierManagement);

        public const string WarehouseManagement =
            nameof(WarehouseManagement);

        public const string StockOperation =
            nameof(StockOperation);

        public const string PurchaseOrderCreate =
            nameof(PurchaseOrderCreate);

        public const string PurchaseOrderApprove =
            nameof(PurchaseOrderApprove);

        public const string PurchaseOrderComplete =
            nameof(PurchaseOrderComplete);

        public const string UserManagement =
            nameof(UserManagement);
    }
}
