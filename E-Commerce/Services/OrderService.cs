using EcomGalaxy.Models;

namespace EcomGalaxy.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemsService _orderItems;
        private readonly IProductService _productService;
        private readonly IPaymentService _paymentService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderService(IOrderRepository orderRepository, IOrderItemsService orderItems, 
            IProductService productService, IPaymentService paymentService, UserManager<ApplicationUser> userManager)
        {
            _orderRepository = orderRepository;
            _orderItems = orderItems;
            _productService = productService;
            _paymentService = paymentService;
            _userManager = userManager;
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

        public async Task<IEnumerable<CustomerOrderItemViewModel>> OrderDetails(int orderId)
        {
            var lstViewModel = new List<CustomerOrderItemViewModel>();
            var orderItems = await _orderItems.GetOrderItemsByOrderIdAsync(orderId);
            foreach (var item in orderItems)
            {
                var product = await _productService.GetProductByIdAsync(item.ProductId);
                var OrdviewModel = new CustomerOrderItemViewModel
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
        
        public async Task<IEnumerable<SellerOrderItemViewModel>> OrderSellerDetails(string userId)
        {
            var lstViewModel = new List<SellerOrderItemViewModel>();
            var orderItems = await _orderItems.GetOrderItemsByUserIdAsync(userId);
            var user = await _userManager.FindByIdAsync(userId);

            foreach (var item in orderItems)
            {
                var product = await _productService.GetProductByIdAsync(item.ProductId);
                var order = await _orderRepository.GetOrderByIdAsync(item.OrderId);
                var OrdviewModel = new SellerOrderItemViewModel
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    ProductImage = product.ProductImagePath[0],
                    ProductName = product.Name,
                    ProductDescription = product.Description,
                    TotalPrice = product.Price * item.Quantity,
                    userCity = user.City,
                    userCountry = user.Country,
                    userStreet = user.Street,
                    userPostalCode = user.PostalCode,
                    userOrderStatus = order.Status

                };
                lstViewModel.Add(OrdviewModel);
            }
            return lstViewModel;
        }

        public async Task<IEnumerable<AdminOrderItemViewModel>> OrderAdminDetails()
        {
            var lstViewModel = new List<AdminOrderItemViewModel>();
            var orderItems = await _orderItems.GetAllOrderItemsAsync();

            foreach (var item in orderItems)
            {
                var product = await _productService.GetProductByIdAsync(item.ProductId);
                var order = await _orderRepository.GetOrderByIdAsync(item.OrderId);
                var OrdviewModel = new AdminOrderItemViewModel
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    ProductImage = product.ProductImagePath[0],
                    ProductName = product.Name,
                    ProductDescription = product.Description,
                    TotalPrice = product.Price * item.Quantity,
                    userOrderStatus = order.Status
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
                ordersMainViewModel.OrderDate = order.OrderedDate;
                ordersMainViewModel.ShippedDate = order.ShippedDate;/////
                ordersMainViewModel.OrderId = order.Id;
                ordersMainViewModel.TotalAmount = order.TotalPrice;
                lstordersMainViewModel.Add(ordersMainViewModel);
            }
            return lstordersMainViewModel;
        }
    }
}
