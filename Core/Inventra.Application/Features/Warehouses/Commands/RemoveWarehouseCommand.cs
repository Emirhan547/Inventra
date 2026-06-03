using Inventra.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Warehouses.Commands
{
    public class RemoveWarehouseCommand:IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
