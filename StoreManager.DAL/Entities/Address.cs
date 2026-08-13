using System.ComponentModel.DataAnnotations;

namespace StoreManager.DAL.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public string? Street { get; set; }
        [Required]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Type { get; set; } = string.Empty;
    }
}
