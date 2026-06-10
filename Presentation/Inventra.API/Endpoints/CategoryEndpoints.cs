using Inventra.Application.Features.Categories.Commands;
using Inventra.Application.Features.Categories.Queries;
using Inventra.Application.Features.Products.Commands;
using Inventra.Domain.Constants;
using MediatR;

namespace Inventra.API.Endpoints
{
    public static class CategoryEndpoints
    {
        public static void MapCategoryEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/categories").WithTags("Categories");

            group.MapPost(
     "/",
     async (
         CreateCategoryCommandRequest request,
         IMediator mediator) =>
     {
         var result =
             await mediator.Send(request);

         if (!result.Success)
         {
             return Results.BadRequest(result);
         }

         return Results.Created(
             $"/categories/{result.Data}",
             result);
     })
     .RequireAuthorization(
         Policies.CategoryManagement);

            group.MapGet(
                "/",
                async (
                    IMediator mediator) =>
                {
                    var result =
                        await mediator.Send(
                            new GetCategoriesQueryRequest());

                    return Results.Ok(result);
                })
                .RequireAuthorization();

            group.MapGet(
                "/{id:guid}",
                async (
                    Guid id,
                    IMediator mediator) =>
                {
                    var result =
                        await mediator.Send(
                            new GetCategoryByIdQueryRequest
                            {
                                Id = id
                            });

                    if (!result.Success)
                    {
                        return Results.NotFound(result);
                    }

                    return Results.Ok(result);
                })
                .RequireAuthorization();

            group.MapPut(
                "/{id:guid}",
                async (
                    Guid id,
                    UpdateCategoryCommand command,
                    IMediator mediator) =>
                {
                    command.Id = id;

                    var result =
                        await mediator.Send(command);

                    if (!result.Success)
                    {
                        return Results.BadRequest(result);
                    }

                    return Results.Ok(result);
                })
                .RequireAuthorization(
                    Policies.CategoryManagement);

            group.MapDelete(
                "/{id:guid}",
                async (
                    Guid id,
                    IMediator mediator) =>
                {
                    var result =
                        await mediator.Send(
                            new RemoveCategoryCommand
                            {
                                Id = id
                            });

                    if (!result.Success)
                    {
                        return Results.NotFound(result);
                    }

                    return Results.Ok(result);
                })
                .RequireAuthorization(
                    Policies.CategoryManagement);
        }
    }
}