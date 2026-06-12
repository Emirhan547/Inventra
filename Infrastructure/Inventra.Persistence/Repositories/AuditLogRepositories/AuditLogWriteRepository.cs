using Inventra.Application.Abstractions.Repositories.AuditLogRepositories;
using Inventra.Domain.Entities;
using Inventra.Persistence.Context;
using Inventra.Persistence.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Persistence.Repositories.AuditLogRepositories
{
    public class AuditLogWriteRepository
    : WriteRepository<AuditLog>,
      IAuditLogWriteRepository
    {
        public AuditLogWriteRepository(
            InventraDbContext context)
            : base(context)
        {
        }
    }
}
