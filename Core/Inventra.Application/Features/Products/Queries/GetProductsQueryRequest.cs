using Inventra.Application.Common.Pagination;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Products.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Products.Queries
{
    public sealed class GetProductsQueryRequest: PagedRequest,IRequest<Result<PagedResponse<GetProductsQueryResponse>>>
    {
        public string? Search { get; set; }

        public Guid? CategoryId { get; set; }
    }
}
