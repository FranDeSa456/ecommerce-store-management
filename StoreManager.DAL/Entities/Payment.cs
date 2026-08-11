using System.ComponentModel.DataAnnotations;

namespace StoreManager.DAL.Entities
{
    internal class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public int Amount { get; set; }
        public DateOnly PaymentDate { get; set; }
        [Required]
        public string PaymentMethod { get; set; } = string.Empty;
    }
}
