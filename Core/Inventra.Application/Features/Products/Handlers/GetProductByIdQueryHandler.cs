using Inventra.Application.Abstractions.Repositories.ProductRepositories;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Products.Queries;
using Inventra.Application.Features.Products.Results;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Products.Handlers
{
    public class GetProductByIdQueryHandler (IProductReadRepository _repository): IRequestHandler<GetProductByIdQueryRequest, Result<GetProductByIdQueryResponse>>
    {
        public async Task<Result<GetProductByIdQueryResponse>> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            if (product == null)
            {
                return Result<GetProductByIdQueryResponse>.Failure("Product Bulunamadı");
            }
            return Result<GetProductByIdQueryResponse>.SuccessResult(product.Adapt<GetProductByIdQueryResponse>());
        }
    }
}
