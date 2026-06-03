using Inventra.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Suppliers.Commands
{
    public class CreateSupplierCommandRequest:IRequest<Result<CreateSupplierCommandResponse>>
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
