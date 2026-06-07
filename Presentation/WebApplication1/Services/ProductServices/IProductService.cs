using Inventra.WebUI.Dtos.ProductDtos;

namespace Inventra.WebUI.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<ResultProductDto>> GetAllAsync();

        Task<UpdateProductDto?> GetByIdAsync(Guid id);

        Task CreateAsync(CreateProductDto model);

        Task UpdateAsync(UpdateProductDto model);

        Task DeleteAsync(Guid id);
    }
}
