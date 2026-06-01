using Inventra.Domain.Common;
using Inventra.Domain.Enums;

namespace Inventra.Domain.Entities
{
    public class StockMovement : BaseEntity
    {
        public Guid StockId { get; set; }

        public Stock Stock { get; set; }

        public StockMovementType Type { get; set; }

        public int Quantity { get; set; }

        public string? Description { get; set; }
    }
}
