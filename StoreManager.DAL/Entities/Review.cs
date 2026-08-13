using System.ComponentModel.DataAnnotations;

namespace StoreManager.DAL.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Rating { get; set; }
        [MaxLength(500)]
        public string? Comment { get; set; }
    }
}
