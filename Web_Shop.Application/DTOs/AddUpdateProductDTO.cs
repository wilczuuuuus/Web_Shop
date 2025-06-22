using System.ComponentModel.DataAnnotations;

namespace Web_Shop.Application.DTOs
{
    public class AddUpdateProductDTO
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(100)]
        public string Sku { get; set; } = string.Empty;
    }
} 