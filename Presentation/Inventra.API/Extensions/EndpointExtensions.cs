using Inventra.API.Endpoints;

namespace Inventra.API.Extensions
{
    public static class EndpointExtensions
    {
        public static void MapEndpoints(
            this WebApplication app)
        {
            app.MapProductEndpoints();
            app.MapWarehouseEndpoints();
            app.MapStockEndpoints();
            app.MapStockMovementEndpoints();
            app.MapSupplierEndpoints();
            app.MapDashboardEndpoints();
           
        }
    }
}
