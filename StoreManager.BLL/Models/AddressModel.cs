using System.ComponentModel.DataAnnotations;

namespace StoreManager.BLL.Models
{
    public class AddressModel : IModelWithId
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string? Street { get; set; }
        [Required]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Type { get; set; } = string.Empty;
    }
}
