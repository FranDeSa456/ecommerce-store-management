using Microsoft.EntityFrameworkCore;

namespace StoreManager.BLL.Models
{
    public class OrderItemModel
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        [Precision(18, 2)]
        public decimal UnitPrice { get; set; }
    }
}
