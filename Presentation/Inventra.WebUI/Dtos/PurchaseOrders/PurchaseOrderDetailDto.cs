namespace Inventra.WebUI.Dtos.PurchaseOrders
{
    public class PurchaseOrderDetailDto
    {
        public Guid Id { get; set; }

        public string OrderNumber { get; set; }

        public Guid SupplierId { get; set; }

        public string SupplierName { get; set; }

        public PurchaseOrderStatus Status { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<PurchaseOrderItemDto> Items
        { get; set; } = [];
    }
}
