namespace Inventra.WebUI.Dtos.ProductDtos
{
    public class UpdateProductDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string SKU { get; set; }

        public decimal UnitPrice { get; set; }

        public int MinimumStockLevel { get; set; }

        public string CategoryName { get; set; }
        public Guid CategoryId { get; set; }
    }
}
