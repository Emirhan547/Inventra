using Inventra.Application.Abstractions.Repositories.GenericRepositories;
using Inventra.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Inventra.Persistence.Repositories
{
    public class ReadRepository<T>
     : IReadRepository<T>
     where T : class
    {
        private readonly InventraDbContext _context;

        public ReadRepository(
            InventraDbContext context)
        {
            _context = context;
        }

        protected DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(
            bool tracking = false)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            return query;
        }

        public IQueryable<T> GetWhere(
            Expression<Func<T, bool>> predicate,
            bool tracking = false)
        {
            var query = Table.Where(predicate);

            if (!tracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<T?> GetSingleAsync(
            Expression<Func<T, bool>> predicate,
            bool tracking = false)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<T?> GetByIdAsync(
            Guid id,
            bool tracking = false)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(x =>
                EF.Property<Guid>(x, "Id") == id);
        }
    }
}
