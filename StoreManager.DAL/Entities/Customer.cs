using System.ComponentModel.DataAnnotations;

namespace StoreManager.DAL.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public ICollection<Order> Orders { get; set; } = [];
        public ICollection<Address> Addresses { get; set; } = [];
        public ICollection<Review> Reviews { get; set; } = [];
    }
}
