namespace EcomGalaxy.ViewModel
{
    public class AddProductViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public double Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be a non-negative integer.")]
        public int StockQuantity { get; set; }

        public List<string>? ProductImagePath { get; set; }

        [Range(0, 5, ErrorMessage = "Average rating must be between 0 and 5.")]
        public double? AverageRating { get; set; }

        [Required]
        public int CategoryId { get; set; }

    }
}
