using Inventra.Application.Abstractions.Repositories.ProductRepositories;
using Inventra.Application.Common.Pagination;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Products.Queries;
using Inventra.Application.Features.Products.Results;
using Mapster;
using MediatR;

namespace Inventra.Application.Features.Products.Handlers;

public class GetProductsQueryHandler: IRequestHandler<GetProductsQueryRequest,Result<PagedResponse<GetProductsQueryResponse>>>
{
    private readonly IProductReadRepository _readRepository;

    public GetProductsQueryHandler(IProductReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<Result<PagedResponse<GetProductsQueryResponse>>>Handle(GetProductsQueryRequest request,CancellationToken cancellationToken)
    {
        var pagedProducts =await _readRepository.GetPagedWithCategoryAsync(request.PageNumber,request.PageSize,cancellationToken);
        var mapped =pagedProducts.Items.Adapt<List<GetProductsQueryResponse>>();
        var response =new PagedResponse<GetProductsQueryResponse>
            {
                Items = mapped,
                PageNumber =pagedProducts.PageNumber,
                PageSize =pagedProducts.PageSize,
                TotalCount =pagedProducts.TotalCount,
                TotalPages =pagedProducts.TotalPages
            };
        return Result<PagedResponse<GetProductsQueryResponse>> .SuccessResult(response);
    }
}