using Inventra.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Inventra.Persistence.Interceptors;

public class AuditInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>>
        SavingChangesAsync(DbContextEventData eventData,InterceptionResult<int> result,CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if (context is null)
        {
            return base.SavingChangesAsync(eventData,result,cancellationToken);
        }
        var entries = context.ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:entry.Entity.CreatedDate =DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                 entry.Entity.UpdatedDate =DateTime.UtcNow;
                    break;
            }
        }

        return base.SavingChangesAsync(eventData,result,cancellationToken);
    }
}