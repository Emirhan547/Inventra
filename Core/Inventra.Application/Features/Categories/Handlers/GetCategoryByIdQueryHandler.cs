using Inventra.Application.Abstractions.Repositories.CategoryRepositories;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Categories.Queries;
using Inventra.Application.Features.Categories.Results;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Categories.Handlers
{
    public class GetCategoryByIdQueryHandler (ICategoryReadRepository _categoryReadRepository): IRequestHandler<GetCategoryByIdQueryRequest, Result<GetCategoryByIdResponse>>
    {
        public async Task<Result<GetCategoryByIdResponse>> Handle(GetCategoryByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var category=await _categoryReadRepository.GetByIdAsync(request.Id);
            if (category == null)
            {
                return Result<GetCategoryByIdResponse>.Failure("Category Not Found");
            }
            return Result<GetCategoryByIdResponse>.SuccessResult(category.Adapt<GetCategoryByIdResponse>());
        }
    }
}
