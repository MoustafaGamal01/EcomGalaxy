using EcomGalaxy.Domain.Models.User;

namespace EcomGalaxy.Domain.Models.Order
{
    public class OrderItem
    {
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public EcomGalaxy.Domain.Models.Product.Product? Product { get; set; }

        [ForeignKey("Seller")]
        public string? SellerId { get; set; }
        public ApplicationUser? Seller { get; set; }
    }
}
