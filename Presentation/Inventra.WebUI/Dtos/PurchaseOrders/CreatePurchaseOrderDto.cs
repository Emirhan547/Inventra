namespace Inventra.WebUI.Dtos.PurchaseOrders
{
    public class CreatePurchaseOrderDto
    {
        public Guid SupplierId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
