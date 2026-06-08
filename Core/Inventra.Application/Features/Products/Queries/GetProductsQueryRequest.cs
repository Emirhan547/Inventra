using Inventra.Application.Common.Results;
using Inventra.Application.Features.Products.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Products.Queries
{
    public class GetProductsQueryRequest: IRequest<Result<List<GetProductsQueryResponse>>>
    {
    }
}
