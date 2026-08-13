using System.ComponentModel.DataAnnotations;

namespace StoreManager.BLL.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
        [MaxLength(500)]
        public string? Comment { get; set; }
    }
}
