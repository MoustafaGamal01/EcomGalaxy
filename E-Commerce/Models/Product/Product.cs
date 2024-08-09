using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using EcomGalaxy.Models.User;

namespace EcomGalaxy.Models.Product
{
    public class Product
    {
        public int Id { get; set; }

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

        //[MaxLength(250)]
        public List<string>? ProductImagePath { get; set; }

        [Range(0, 5, ErrorMessage = "Average rating must be between 0 and 5.")]
        public double? AverageRating { get; set; }

        // Navigational Props
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        // for Seller
        [ForeignKey("ApplicationUser")]
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }

        public virtual ICollection<Review>? Reviews { get; set; }
        //public virtual ICollection<Image>? Images { get; set; }
        public Product()
        {
            //Images = new List<Image>();
            Reviews = new List<Review>();
        }
    }
}
