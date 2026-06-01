using Inventra.Application.Abstractions.Repositories.ProductRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Products.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Products.Handlers
{
    public class RemoveProductCommandHandler(IProductReadRepository _productRead,IProductWriteRepository _productWrite,IUnitOfWork _unitOfWork ) : IRequestHandler<RemoveProductCommand, Result>
    {
        public async Task<Result> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRead.GetByIdAsync(request.Id);
            if (product == null) 
            {
                return Result.Failure("Product Bulunamadı");
            }
            _productWrite.Remove(product);
            await _unitOfWork.SaveChangeAsync();
            return Result.SuccessResult();
        }
    }
}
