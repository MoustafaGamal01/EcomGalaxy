using EcomGalaxy.Models.User;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcomGalaxy.Models.Product
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Message { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        // Navigational Props
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [ValidateNever]
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
