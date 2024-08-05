using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EcomGalaxy.Models
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }

    public class Order
    {
        public int Id { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total price must be a non-negative value.")]
        public double TotalPrice { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        // Navigational Props
        [ForeignKey("Payment")]
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }

        [ForeignKey("ShoppingCart")]
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        [ForeignKey("Customer")]
        public string? CustomerId { get; set; }
        public ApplicationUser? Customer { get; set; }

        [ForeignKey("Seller")]
        public string? SellerId { get; set; }
        public ApplicationUser Seller { get; set; }
    }
}
