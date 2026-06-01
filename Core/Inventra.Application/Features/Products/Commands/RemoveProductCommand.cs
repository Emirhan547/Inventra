using Inventra.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Products.Commands
{
    public class RemoveProductCommand:IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
