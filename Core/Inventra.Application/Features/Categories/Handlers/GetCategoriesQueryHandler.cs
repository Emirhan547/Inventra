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
    public class GetCategoriesQueryHandler(ICategoryReadRepository _categoryReadRepository) : IRequestHandler<GetCategoriesQueryRequest, Result<List<GetCategoriesQueryResponse>>>
    {
        public async Task<Result<List<GetCategoriesQueryResponse>>> Handle(GetCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            var categories = await _categoryReadRepository.GetAllAsync();
            var mapped = categories.Adapt<List<GetCategoriesQueryResponse>>();
            return Result<List<GetCategoriesQueryResponse>>.SuccessResult(mapped);
        }
    }
}
