using Inventra.API.Middlewares;

namespace Inventra.API.Extensions
{
    public static class CorrelationIdExtensions
    {
        public static IApplicationBuilder
            UseCorrelationId(
                this IApplicationBuilder app)
        {
            return app.UseMiddleware<
                CorrelationIdMiddleware>();
        }
    }
}
