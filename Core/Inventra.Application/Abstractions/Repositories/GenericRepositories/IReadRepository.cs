using Inventra.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Inventra.Application.Abstractions.Repositories.GenericRepositories
{
    public interface IReadRepository<T>
    where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id,bool tracking = false,CancellationToken cancellationToken = default);

        Task<List<T>> GetAllAsync(bool tracking = false,CancellationToken cancellationToken = default);

        Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> predicate,bool tracking = false,CancellationToken cancellationToken = default);

        Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate,bool tracking = false,CancellationToken cancellationToken = default);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate,CancellationToken cancellationToken = default);
    }
}