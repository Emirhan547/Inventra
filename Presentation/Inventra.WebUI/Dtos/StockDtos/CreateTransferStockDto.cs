namespace Inventra.WebUI.Dtos.StockDtos
{
    public class CreateTransferStockDto
    {
        public Guid ProductId { get; set; }

        public Guid SourceWarehouseId { get; set; }

        public Guid DestinationWarehouseId { get; set; }

        public int Quantity { get; set; }

        public string? Description { get; set; }
    }
}
