using Inventra.WebUI.Dtos.DashboardDtos;

namespace Inventra.WebUI.Services.DashboardServices
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto?>GetDashboardAsync();
    }
}
