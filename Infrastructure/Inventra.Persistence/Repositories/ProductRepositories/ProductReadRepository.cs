using Inventra.Application.Abstractions.Repositories.ProductRepositories;
using Inventra.Domain.Entities;
using Inventra.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
namespace Inventra.Persistence.Repositories.ProductRepositories
{
    public class ProductReadRepository
     : ReadRepository<Product>,
       IProductReadRepository
    {
        public ProductReadRepository(
            InventraDbContext context)
            : base(context)
        {

        }

        public async Task<bool> IsSkuExistsAsync(
     string sku,
     CancellationToken cancellationToken)
        {
            return await Table
                .AnyAsync(x => x.SKU == sku, cancellationToken);
        }
    }
}