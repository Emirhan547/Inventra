using Serilog.Context;
using System.Security.Claims;

namespace Inventra.API.Middlewares
{
    public sealed class UserLogContextMiddleware
    {
        private readonly RequestDelegate _next;

        public UserLogContextMiddleware(
            RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context)
        {
            var userId =
                context.User.FindFirst(
                    ClaimTypes.NameIdentifier)?.Value;

            var userName =
                context.User.FindFirst(
                    ClaimTypes.Name)?.Value;

            using (LogContext.PushProperty(
                       "UserId",
                       userId ?? "Anonymous"))
            using (LogContext.PushProperty(
                       "UserName",
                       userName ?? "Anonymous"))
            {
                await _next(context);
            }
        }
    }
}