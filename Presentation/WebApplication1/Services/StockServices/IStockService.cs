using Inventra.WebUI.Dtos.StockDtos;

namespace Inventra.WebUI.Services.StockServices
{
    public interface IStockService
    {
        Task<List<ResultStockDto>>
            GetAllAsync();
        Task StockInAsync(
    CreateStockInDto model);
        Task StockOutAsync(
    CreateStockOutDto model);
        Task TransferAsync(
    CreateTransferStockDto model);
    }
}
