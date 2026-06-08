using Inventra.Application.Abstractions.Repositories.PurchaseOrderRepositories;
using Inventra.Domain.Entities;
using Inventra.Persistence.Context;
using Inventra.Persistence.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Persistence.Repositories.PurchaseOrderRepositories
{
    public class PurchaseOrderWriteRepository: WriteRepository<PurchaseOrder>,IPurchaseOrderWriteRepository
    {
        public PurchaseOrderWriteRepository(InventraDbContext context): base(context)
        {
        }
    }
}