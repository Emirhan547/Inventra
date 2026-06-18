namespace Inventra.WebUI.Dtos.StockMovementDtos
{
    public sealed class StockMovementFilterDto
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public StockMovementType? Type { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
