using Inventra.WebUI.Dtos.StockMovementDtos;

namespace Inventra.WebUI.Services.StockMovementServices
{
    public interface IStockMovementService
    {
        Task<List<ResultStockMovementDto>>GetAllAsync();
    }
}
