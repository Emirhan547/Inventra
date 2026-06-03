using Inventra.Application.Abstractions.Repositories.ProductRepositories;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Products.Queries;
using Inventra.Application.Features.Products.Results;
using Mapster;
using MediatR;

namespace Inventra.Application.Features.Products.Handlers;

public class GetProductsQueryHandler
    : IRequestHandler<
        GetProductsQueryRequest,
        Result<List<GetProductsQueryResponse>>>
{
    private readonly IProductReadRepository _readRepository;

    public GetProductsQueryHandler(
        IProductReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<
        Result<List<GetProductsQueryResponse>>>
        Handle(
            GetProductsQueryRequest request,
            CancellationToken cancellationToken)
    {
        var products =await _readRepository.GetAllAsync();
        var mapped = products.Adapt<List<GetProductsQueryResponse>>();

        return Result<List<GetProductsQueryResponse>>
            .SuccessResult(mapped);
    }
}