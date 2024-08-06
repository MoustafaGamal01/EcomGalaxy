using EcomGalaxy.Models;
using EcomGalaxy.Services.IServices;

namespace EcomGalaxy.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemsService _orderItems;
        private readonly IProductService _productService;
        private readonly IPaymentService _paymentService;

        public OrderService(IOrderRepository orderRepository, IOrderItemsService orderItems, 
            IProductService productService, IPaymentService paymentService)
        {
            this._orderRepository = orderRepository;
            this._orderItems = orderItems;
            this._productService = productService;
            this._paymentService = paymentService;
        }

        public async Task<bool?> AddOrderAsync(Order order)
        {
            return await _orderRepository.AddOrderAsync(order);
        }

        public async Task<bool?> DeleteOrderAsync(int orderId)
        {
            return await _orderRepository.DeleteOrderAsync(orderId);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _orderRepository.GetOrdersByUserIdAsync(userId);
        }

        public async Task<bool?> UpdateOrderAsync(int orderId, Order order)
        {
            return await _orderRepository.UpdateOrderAsync(orderId, order);
        }

        public async Task<IEnumerable<OrderItemViewModel>> OrderDetails(int orderId)
        {
            var lstViewModel = new List<OrderItemViewModel>();
            var orderItems = await _orderItems.GetOrderItemsByOrderIdAsync(orderId);
            foreach (var item in orderItems)
            {
                var product = await _productService.GetProductByIdAsync(item.ProductId);
                var OrdviewModel = new OrderItemViewModel
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    ProductImage = product.ProductImagePath[0],
                    ProductName = product.Name,
                    ProductDescription = product.Description,
                    TotalPrice = product.Price * item.Quantity
                };
                lstViewModel.Add(OrdviewModel);
            }
            return lstViewModel;
        }

        public async Task<IEnumerable<OrdersMainViewModel>> CustomerOrders(string userId)
        {
            var items = await _orderRepository.GetOrdersByUserIdAsync(userId);
            var lstordersMainViewModel = new List<OrdersMainViewModel>();

            foreach (var order in items)
            {
                var ordersMainViewModel = new OrdersMainViewModel();
                var OrderItems = await _orderItems.GetOrderItemsByOrderIdAsync(order.Id);
                ordersMainViewModel.ProductsCount = OrderItems.Count;
                ordersMainViewModel.OrderStatus = order.Status;
                var pay = await _paymentService.GetPaymentByIdAsync(order.PaymentId);
                ordersMainViewModel.PaymentStatus = pay.Status;
                ordersMainViewModel.OrderDate = order.Date;
                ordersMainViewModel.OrderId = order.Id;
                ordersMainViewModel.TotalAmount = order.TotalPrice;
                lstordersMainViewModel.Add(ordersMainViewModel);
            }
            return lstordersMainViewModel;
        }
    }
}
