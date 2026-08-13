using System.ComponentModel.DataAnnotations;

namespace StoreManager.DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public ICollection<Review> Reviews { get; set; } = [];
        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}
