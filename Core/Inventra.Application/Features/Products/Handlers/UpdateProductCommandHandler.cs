using Inventra.Application.Abstractions.Repositories.ProductRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Products.Commands;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Products.Handlers
{
    public class UpdateProductCommandHandler (IProductReadRepository _readRepository,IProductWriteRepository _writeRepository,IUnitOfWork _unitOfWork): IRequestHandler<UpdateProductCommand, Result>
    {
        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _readRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                return Result.Failure("Product Bulunamadı");

            }
            product = request.Adapt(product);
            _writeRepository.Update(product);
             await _unitOfWork.SaveChangeAsync();
            return Result.SuccessResult();
        }
    }
}
