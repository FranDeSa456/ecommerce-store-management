using Microsoft.EntityFrameworkCore;

namespace StoreManager.DAL.Entities
{
    public class OrderItem
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public int Quantity { get; set; }
        [Precision(18, 2)]
        public decimal UnitPrice { get; set; }
    }
}
