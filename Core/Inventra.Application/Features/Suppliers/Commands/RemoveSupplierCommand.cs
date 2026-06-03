using Inventra.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Suppliers.Commands
{
    public class RemoveSupplierCommand:IRequest<Result>
    {
        public Guid Id { get; set; }

        public RemoveSupplierCommand(Guid id)
        {
            Id = id;
        }
    }
}
