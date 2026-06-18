namespace Inventra.WebUI.Dtos.PurchaseOrders
{
    public class ResultPurchaseOrderDto
    {
        public Guid Id { get; set; }

        public string OrderNumber { get; set; }

        public string SupplierName { get; set; }

        public PurchaseOrderStatus Status { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
