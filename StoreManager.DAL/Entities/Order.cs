using System.ComponentModel.DataAnnotations;

namespace StoreManager.DAL.Entities
{
    internal class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public DateOnly OrderDate { get; set; }
        [Required]
        public string Status { get; set; } = string.Empty;
        public ICollection<OrderItem> OrderItems { get; set; } = [];
        public Payment Payment { get; set; } = null!;
    }
}
