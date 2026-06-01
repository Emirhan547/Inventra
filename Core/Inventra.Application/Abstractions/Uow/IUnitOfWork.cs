using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Abstractions.Uow
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangeAsync();
    }
}
