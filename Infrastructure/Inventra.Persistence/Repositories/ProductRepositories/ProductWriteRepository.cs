using Inventra.Application.Abstractions.Repositories.ProductRepositories;
using Inventra.Domain.Entities;
using Inventra.Persistence.Context;
using Inventra.Persistence.Repositories.GenericRepositories;


namespace Inventra.Persistence.Repositories.ProductRepositories
{
    public class ProductWriteRepository : WriteRepository<Product>,IProductWriteRepository
    {
        public ProductWriteRepository(InventraDbContext context): base(context)
        {
        }
    }
}