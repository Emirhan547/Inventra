namespace Inventra.WebUI.Dtos.StockDtos
{
    public class CreateStockInDto
    {
        public Guid ProductId { get; set; }

        public Guid WarehouseId { get; set; }

        public int Quantity { get; set; }

        public string? Description { get; set; }
    }
}
