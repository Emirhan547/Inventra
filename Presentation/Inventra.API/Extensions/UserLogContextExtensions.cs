using Inventra.API.Middlewares;

namespace Inventra.API.Extensions;

public static class UserLogContextExtensions
{
    public static IApplicationBuilder
        UseUserLogContext(
            this IApplicationBuilder app)
    {
        return app.UseMiddleware<
            UserLogContextMiddleware>();
    }
}