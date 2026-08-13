using System.ComponentModel.DataAnnotations;

namespace StoreManager.BLL.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public List<ProductModel> Products { get; set; } = [];
    }
}
