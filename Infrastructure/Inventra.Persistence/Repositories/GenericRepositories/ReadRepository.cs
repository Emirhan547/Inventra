using Inventra.Application.Abstractions.Repositories.GenericRepositories;
using Inventra.Domain.Common;
using Inventra.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Inventra.Persistence.Repositories.GenericRepositories
{
    public class ReadRepository<T> : IReadRepository<T>
     where T : BaseEntity
    {
        protected readonly InventraDbContext _context;

        public ReadRepository(InventraDbContext context)
        {
            _context = context;
        }

        protected DbSet<T> Table =>_context.Set<T>();

        public async Task<T?> GetByIdAsync(Guid id,bool tracking = false,CancellationToken cancellationToken = default)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
        }

        public async Task<List<T>> GetAllAsync(bool tracking = false,CancellationToken cancellationToken = default)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.ToListAsync(cancellationToken);
        }

        public async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> predicate,bool tracking = false,CancellationToken cancellationToken = default)
        {
            var query = Table.Where(predicate);
            if (!tracking)
                query = query.AsNoTracking();
            return await query.ToListAsync(cancellationToken);
        }

        public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate,bool tracking = false,CancellationToken cancellationToken = default)
        {
            var query = Table.Where(predicate);
            if (!tracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate,CancellationToken cancellationToken = default)
        {
            return await Table.AnyAsync(   predicate,cancellationToken);
        }
    }
}
