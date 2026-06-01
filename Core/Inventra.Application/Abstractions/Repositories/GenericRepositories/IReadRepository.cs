using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Inventra.Application.Abstractions.Repositories.GenericRepositories
{
    public interface IReadRepository<T>
     where T : class
    {
        IQueryable<T> GetAll(
            bool tracking = false);

        IQueryable<T> GetWhere(
            Expression<Func<T, bool>> predicate,
            bool tracking = false);

        Task<T?> GetSingleAsync(
            Expression<Func<T, bool>> predicate,
            bool tracking = false);

        Task<T?> GetByIdAsync(
            Guid id,
            bool tracking = false);
    }
}
