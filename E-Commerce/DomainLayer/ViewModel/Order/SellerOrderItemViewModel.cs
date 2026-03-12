using EcomGalaxy.Domain.Models.Order;

namespace EcomGalaxy.ViewModel.Order
{
    public class SellerOrderItemViewModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double TotalPrice { get; set; }
        public string userCity { get; set; }
        public string userCountry { get; set; }
        public string userStreet { get; set; }
        public string userPostalCode { get; set; }
        public OrderStatus userOrderStatus { get; set; }
    }
}
