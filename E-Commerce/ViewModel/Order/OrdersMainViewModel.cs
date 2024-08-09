using EcomGalaxy.Models.Payment;

namespace EcomGalaxy.ViewModel.Order
{
    public class OrdersMainViewModel
    {
        public int OrderId { get; set; }
        public int ProductsCount { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }

        public OrderStatus OrderStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public double TotalAmount { get; set; }
    }
}
