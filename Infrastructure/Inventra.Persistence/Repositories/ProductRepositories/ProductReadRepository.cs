using Inventra.Application.Abstractions.Repositories.ProductRepositories;
using Inventra.Application.Common.Pagination;
using Inventra.Domain.Entities;
using Inventra.Persistence.Context;
using Inventra.Persistence.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
namespace Inventra.Persistence.Repositories.ProductRepositories
{
    public class ProductReadRepository: ReadRepository<Product>,IProductReadRepository
    {
        public ProductReadRepository(InventraDbContext context): base(context)
        {

        }

        public async Task<bool> IsSkuExistsAsync(string sku,CancellationToken cancellationToken)
        {
            return await Table.AnyAsync(x => x.SKU == sku, cancellationToken);
        }
        public async Task<PagedResponse<Product>>GetPagedWithCategoryAsync(int pageNumber,int pageSize,string? search,Guid? categoryId,CancellationToken cancellationToken = default)
        {
            IQueryable<Product> query = Table.AsNoTracking().Include(x => x.Category);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x =>x.Name.Contains(search) ||x.SKU.Contains(search));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(x =>x.CategoryId == categoryId.Value);
            }

            var totalCount =await query.CountAsync(cancellationToken);

            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return new PagedResponse<Product>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages =(int)Math.Ceiling(totalCount /(double)pageSize)
            };
        }
    }
}