using Inventra.WebUI.Common;
using Inventra.WebUI.Dtos.ProductDtos;

namespace Inventra.WebUI.Services.ProductServices
{
    public interface IProductService
    {
        Task<PagedResponse<ResultProductDto>>GetAllAsync(ProductFilterDto? filter = null);
        Task<UpdateProductDto?> GetByIdAsync(Guid id);

        Task CreateAsync(CreateProductDto model);

        Task UpdateAsync(UpdateProductDto model);

        Task DeleteAsync(Guid id);
    }
}
