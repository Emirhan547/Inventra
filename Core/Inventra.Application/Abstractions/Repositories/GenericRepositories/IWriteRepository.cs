using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Abstractions.Repositories.GenericRepositories
{
    public interface IWriteRepository<T>
    where T : class
    {
        Task AddAsync(T entity);

        Task AddRangeAsync(List<T> entities);

        void Remove(T entity);

        void RemoveRange(List<T> entities);

        void Update(T entity);

        Task<int> SaveAsync();
    }
}
