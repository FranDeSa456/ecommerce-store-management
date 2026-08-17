using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace StoreManager.DAL.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        public DateOnly PaymentDate { get; set; }
        [Required]
        [MaxLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;
    }
}
