namespace Inventra.WebUI.Dtos.DashboardDtos
{
    public sealed class DashboardMovementChartDto
    {
        public string Date { get; set; } = default!;

        public int StockInCount { get; set; }

        public int StockOutCount { get; set; }
    }
}
