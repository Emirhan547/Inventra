using Inventra.Application.Abstractions.Repositories.CategoryRepositories;
using Inventra.Application.Abstractions.Repositories.ProductRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Products.Commands;
using Inventra.Domain.Entities;
using Mapster;
using MediatR;

namespace Inventra.Application.Features.Products.Handlers
{
    public class CreateProductCommandHandler (IProductWriteRepository _writeRepository,IProductReadRepository _readRepository,IUnitOfWork _unitOfWork,ICategoryReadRepository _categoryReadRepository): IRequestHandler<CreateProductCommandRequest, Result<CreateProductCommandResponse>>
    {
        public async Task<Result<CreateProductCommandResponse>>Handle(CreateProductCommandRequest request,CancellationToken cancellationToken)
        {
            var isSkuExists = await _readRepository.IsSkuExistsAsync(request.SKU,cancellationToken);
            if (isSkuExists)
            {
                return Result<CreateProductCommandResponse>.Failure("SKU already exists");
            }
            var categoryExists =await _categoryReadRepository.AnyAsync(x => x.Id == request.CategoryId,cancellationToken);
            if (!categoryExists)
            {
                return Result<CreateProductCommandResponse>.Failure("Category not found.");
            }
            var product = request.Adapt<Product>();
            await _writeRepository.AddAsync(product);
            await _unitOfWork.SaveChangeAsync();
            return Result<CreateProductCommandResponse>.SuccessResult(new CreateProductCommandResponse
                    {
                        Id = product.Id
                    },
                    "Product created successfully");
        }
    }
}
