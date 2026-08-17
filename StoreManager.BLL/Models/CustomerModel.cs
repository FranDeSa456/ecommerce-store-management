using System.ComponentModel.DataAnnotations;

namespace StoreManager.BLL.Models
{
    public class CustomerModel : IModelWithId
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public List<OrderModel> Orders { get; set; } = [];
        public List<AddressModel> Addresses { get; set; } = [];
        public List<ReviewModel> Reviews { get; set; } = [];
    }
}
