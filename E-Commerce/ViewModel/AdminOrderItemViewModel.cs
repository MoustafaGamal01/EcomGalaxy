namespace EcomGalaxy.ViewModel
{
    public class AdminOrderItemViewModel
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
        public OrderStatus userOrderStatus { get; set; }
    }
}