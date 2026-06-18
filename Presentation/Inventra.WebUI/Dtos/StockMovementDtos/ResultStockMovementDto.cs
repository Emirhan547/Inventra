namespace Inventra.WebUI.Dtos.StockMovementDtos
{
    public class ResultStockMovementDto
    {
        public Guid Id { get; set; }

        public Guid StockId { get; set; }

        public string ProductName { get; set; }

        public string WarehouseName { get; set; }

        public StockMovementType Type { get; set; }

        public int Quantity { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
