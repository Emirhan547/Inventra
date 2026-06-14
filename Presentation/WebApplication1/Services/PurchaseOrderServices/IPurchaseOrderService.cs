using Inventra.WebUI.Common;
using Inventra.WebUI.Dtos.PurchaseOrders;

namespace Inventra.WebUI.Services.PurchaseOrderServices
{
    public interface IPurchaseOrderService
    {
        Task<PagedResponse<ResultPurchaseOrderDto>> GetAllAsync();

        Task<PurchaseOrderDetailDto?>GetByIdAsync(Guid id);

        Task CreateAsync(CreatePurchaseOrderDto model);

        Task ApproveAsync(Guid id);

        Task CompleteAsync(CompletePurchaseOrderDto model);
    }
}
