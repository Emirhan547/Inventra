using Inventra.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Categories.Commands
{
    public class RemoveCategoryCommand:IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
