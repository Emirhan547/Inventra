using Inventra.Application.Abstractions.Repositories.GenericRepositories;
using Inventra.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Persistence.Repositories
{
    public class WriteRepository<T>
     : IWriteRepository<T>
     where T : class
    {
        private readonly InventraDbContext _context;

        public WriteRepository(
            InventraDbContext context)
        {
            _context = context;
        }

        protected DbSet<T> Table => _context.Set<T>();

        public async Task AddAsync(T entity)
            => await Table.AddAsync(entity);

        public async Task AddRangeAsync(List<T> entities)
            => await Table.AddRangeAsync(entities);

        public void Remove(T entity)
            => Table.Remove(entity);

        public void RemoveRange(List<T> entities)
            => Table.RemoveRange(entities);

        public void Update(T entity)
            => Table.Update(entity);

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
