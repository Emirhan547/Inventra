using Inventra.Application.Abstractions.Repositories.GenericRepositories;
using Inventra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Abstractions.Repositories.StockRepositories
{
    public interface IStockWriteRepository:IWriteRepository<Stock>
    {
    }
}
