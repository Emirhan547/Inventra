using Inventra.Application.Abstractions.Repositories.CategoryRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Categories.Commands;
using Inventra.Domain.Entities;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Categories.Handlers
{
    public class CreateCategoryCommandHandler (ICategoryWriteRepository _categoryWriteRepository,IUnitOfWork _unitOfWork): IRequestHandler<CreateCategoryCommandRequest, Result<CreateCategoryCommandResponse>>
    {
        public async Task<Result<CreateCategoryCommandResponse>> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var mapped = request.Adapt<Category>();
            await _categoryWriteRepository.AddAsync(mapped);
            await _unitOfWork.SaveChangeAsync();
            return Result<CreateCategoryCommandResponse>.SuccessResult(new CreateCategoryCommandResponse
            {
                Id = mapped.Id,
            },
                "Category Success Added");
        }
    }
}
