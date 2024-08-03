using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcomGalaxy.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string PaymentMethod { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a non-negative value.")]
        public double Amount { get; set; }

        [Required]
        [MaxLength(16)]
        [MinLength(13)]
        public string CardNumber { get; set; }

        [Required]
        [MaxLength(3)]
        public string CVV { get; set; }

        [Required]
        [MaxLength(5)]
        public string ExpiryDate { get; set; }

        // Navigational Props
        [ValidateNever]
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
