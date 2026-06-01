using Inventra.Application.Abstractions.Uow;
using Inventra.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Persistence.Uow
{
    public class UnitOfWork (InventraDbContext _context): IUnitOfWork
    {
        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
