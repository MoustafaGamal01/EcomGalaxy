using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcomGalaxy.Models
{
    public enum PaymentStatus
    {
        Paid,
        WhenHanded,
        Declined
    }
    public class Payment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a non-negative value.")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Card Number is required.")]
        [MinLength(13, ErrorMessage = "Card Number must be at least 13 characters long.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "CVV is required.")]
        [MaxLength(3, ErrorMessage = "CVV cannot be longer than 3 characters.")]
        public string CVV { get; set; }

        [Required(ErrorMessage = "Expiry Date is required.")]
        [MaxLength(5, ErrorMessage = "Expiry Date cannot be longer than 5 characters.")]
        public string ExpiryDate { get; set; }

        public PaymentStatus Status { get; set; }  

        public DateTime PaymentDate { get; set; } 
        
        // Navigational Props
        [ValidateNever]
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }

    }
}
