using Inventra.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Categories.Commands
{
    public class UpdateCategoryCommand:IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
