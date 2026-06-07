using Inventra.Application.Common.Results;
using Inventra.Application.Features.Categories.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Categories.Queries
{
    public class GetCategoryByIdQueryRequest:IRequest<Result<GetCategoryByIdResponse>>
    {
        public Guid Id { get; set; }
    }
}
