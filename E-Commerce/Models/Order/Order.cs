using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using EcomGalaxy.Models.User;

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
        public DateTime OrderedDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public ApplicationUser? Customer { get; set; }

        [ForeignKey("ShoppingCart")]
        public int ShoppingCartId { get; set; }
        public EcomGalaxy.Models.ShoppingCart.ShoppingCart? ShoppingCart { get; set; }

        [ForeignKey("Payment")]
        public int PaymentId { get; set; }
        public EcomGalaxy.Models.Payment.Payment? Payment { get; set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
