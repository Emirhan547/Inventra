using Inventra.Application.Abstractions.Repositories.GenericRepositories;
using Inventra.Application.Common.Pagination;
using Inventra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Abstractions.Repositories.ProductRepositories
{
    public interface IProductReadRepository
     : IReadRepository<Product>
    {
        Task<bool> IsSkuExistsAsync(string sku,CancellationToken cancellationToken);
        Task<PagedResponse<Product>>GetPagedWithCategoryAsync(int pageNumber, int pageSize,string? search,Guid? categoryId,CancellationToken cancellationToken = default);
    }
}
