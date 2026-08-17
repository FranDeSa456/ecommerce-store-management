using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace StoreManager.BLL.Models
{
    public class ProductModel : IModelWithId
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public List<ReviewModel> Reviews { get; set; } = [];
        public List<OrderItemModel> OrderItems { get; set; } = [];
    }
}
