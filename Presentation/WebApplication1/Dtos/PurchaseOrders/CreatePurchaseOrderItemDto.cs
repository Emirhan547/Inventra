namespace Inventra.WebUI.Dtos.PurchaseOrders
{
    public class CreatePurchaseOrderItemDto
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
