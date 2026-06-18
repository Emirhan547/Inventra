namespace Inventra.WebUI.Dtos.PurchaseOrders
{
    public class PurchaseOrderFilterDto
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public PurchaseOrderStatus? Status { get; set; }

        public Guid? SupplierId { get; set; }
    }
}
