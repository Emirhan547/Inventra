namespace Inventra.WebUI.Dtos.StockDtos
{
    public class ResultStockDto
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public Guid WarehouseId { get; set; }

        public string WarehouseName { get; set; }

        public int Quantity { get; set; }

        // AI

        public int PredictedConsumption30Days { get; set; }

        public string RiskLevel { get; set; } = string.Empty;
        public int RecommendedOrderQuantity
        {
            get;
            set;
        }
    }
}
