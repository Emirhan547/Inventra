using FluentValidation;
using Inventra.Application.Common.Results;
using System.Text.Json;

namespace Inventra.API.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            context.Response.ContentType = "application/json";

            Result response;

            switch (exception)
            {
                case ValidationException validationException:

                    context.Response.StatusCode =
                        StatusCodes.Status400BadRequest;

                    response = Result.Failure(
                        validationException.Errors
                            .Select(x => x.ErrorMessage)
                            .ToList());

                    break;

                default:

                    context.Response.StatusCode =
                        StatusCodes.Status500InternalServerError;

                    response = Result.Failure(
                        "An unexpected error occurred");

                    break;
            }

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
