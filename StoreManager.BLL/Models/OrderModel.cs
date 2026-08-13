using System.ComponentModel.DataAnnotations;

namespace StoreManager.BLL.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateOnly OrderDate { get; set; }
        [Required]
        public string Status { get; set; } = string.Empty;
        public List<OrderItemModel> OrderItems { get; set; } = [];
        public PaymentModel? Payment { get; set; } = null!;
    }
}
