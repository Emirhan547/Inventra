using Inventra.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Warehouses.Commands
{
    public class CreateWarehouseCommandRequest:IRequest<Result<CreateWarehouseCommandResponse>>
    {
        public string Name { get; set; }

        public string Location { get; set; }
    }
}
