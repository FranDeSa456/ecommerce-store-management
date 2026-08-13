using System.ComponentModel.DataAnnotations;

namespace StoreManager.BLL.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateOnly PaymentDate { get; set; }
        [Required]
        public string PaymentMethod { get; set; } = string.Empty;
    }
}
