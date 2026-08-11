using System.ComponentModel.DataAnnotations;

namespace StoreManager.DAL.Entities
{
    internal class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public ICollection<Product> Products { get; set; } = [];
    }
}
