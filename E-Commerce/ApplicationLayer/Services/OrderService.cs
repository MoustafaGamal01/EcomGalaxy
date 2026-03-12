using EcomGalaxy.ApplicationLayer.Services.IServices;
using EcomGalaxy.DataAccess.Repositories.IRepository;
using EcomGalaxy.Domain.Models.Order;
using EcomGalaxy.Domain.Models.User;
using EcomGalaxy.ViewModel.Order;

namespace EcomGalaxy.ApplicationLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly IProductService _productService;
        private readonly IPaymentService _paymentService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderService(
            IOrderRepository orderRepository,
            IOrderItemsRepository orderItemsRepository,
            IProductService productService,
            IPaymentService paymentService,
            UserManager<ApplicationUser> userManager)
        {
            _orderRepository = orderRepository;
            _orderItemsRepository = orderItemsRepository;
            _productService = productService;
            _paymentService = paymentService;
            _userManager = userManager;
        }

        // ── Write ────────────────────────────────────────────────────────────────

        public async Task AddOrderAsync(Order order)
            => await _orderRepository.AddOrderAsync(order);

        public async Task UpdateOrderAsync(int orderId, Order order)
            => await _orderRepository.UpdateOrderAsync(orderId, order);

        public async Task DeleteOrderAsync(int orderId)
            => await _orderRepository.DeleteOrderAsync(orderId);

        // ── Read ─────────────────────────────────────────────────────────────────

        public async Task<Order?> GetOrderByIdAsync(int orderId)
            => await _orderRepository.GetOrderByIdAsync(orderId);

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
            => await _orderRepository.GetAllOrdersAsync();

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
            => await _orderRepository.GetOrdersByUserIdAsync(userId);

        // ── Business operations ──────────────────────────────────────────────────

        /// <summary>
        /// Cancels an order: restores stock for each item, then
        /// deletes the items, the order, and its payment.
        /// </summary>
        public async Task CancelOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId)
                ?? throw new KeyNotFoundException($"Order {orderId} not found.");

            var orderItems = await _orderItemsRepository.GetOrderItemsByOrderIdAsync(orderId);

            var productIds = orderItems.Select(i => i.ProductId).Distinct().ToList();
            var products = await _productService.GetProductsByIdsAsync(productIds);
            var productMap = products.ToDictionary(p => p.Id);

            foreach (var item in orderItems)
            {
                if (productMap.TryGetValue(item.ProductId, out var product))
                {
                    product.StockQuantity += item.Quantity;
                    await _productService.UpdateProductAsync(product.Id, product);
                }
                await _orderItemsRepository.DeleteOrderItemAsync(item.Id);
            }

            await _orderRepository.DeleteOrderAsync(orderId);
            await _paymentService.DeletePaymentAsync(order.PaymentId);
        }

        public async Task ShipOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId)
                ?? throw new KeyNotFoundException($"Order {orderId} not found.");

            order.Status = OrderStatus.Shipped;
            order.ShippedDate = DateTime.UtcNow;
            await _orderRepository.UpdateOrderAsync(orderId, order);
        }

        public async Task ReceivedOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId)
                ?? throw new KeyNotFoundException($"Order {orderId} not found.");

            var orderItems = await _orderItemsRepository.GetOrderItemsByOrderIdAsync(orderId);

            foreach (var item in orderItems)
                await _orderItemsRepository.DeleteOrderItemAsync(item.Id);

            await _orderRepository.DeleteOrderAsync(orderId);
            await _paymentService.DeletePaymentAsync(order.PaymentId);
        }

        public async Task<IEnumerable<OrdersMainViewModel>> CustomerOrders(string userId)
        {
            var orders = (await _orderRepository.GetOrdersByUserIdAsync(userId)).ToList();
            if (!orders.Any()) return Enumerable.Empty<OrdersMainViewModel>();

            var orderIds = orders.Select(o => o.Id).ToList();
            var allItems = await _orderItemsRepository.GetOrderItemsByOrderIdsAsync(orderIds);
            var itemCountMap = allItems
                .GroupBy(i => i.OrderId)
                .ToDictionary(g => g.Key, g => g.Count());

            var paymentIds = orders.Select(o => o.PaymentId).Distinct().ToList();
            var payments = await _paymentService.GetPaymentsByIdsAsync(paymentIds);
            var paymentMap = payments.ToDictionary(p => p.Id);

            return orders.Select(order =>
            {
                paymentMap.TryGetValue(order.PaymentId, out var payment);
                return new OrdersMainViewModel
                {
                    OrderId = order.Id,
                    OrderStatus = order.Status,
                    PaymentStatus = payment?.Status ?? default,
                    OrderDate = order.OrderedDate,
                    ShippedDate = order.ShippedDate,
                    TotalAmount = order.TotalPrice,
                    ProductsCount = itemCountMap.TryGetValue(order.Id, out var count) ? count : 0
                };
            }).ToList();
        }

        public async Task<IEnumerable<CustomerOrderItemViewModel>> OrderDetails(int orderId)
        {
            var orderItems = await _orderItemsRepository.GetOrderItemsByOrderIdAsync(orderId);
            if (!orderItems.Any()) return Enumerable.Empty<CustomerOrderItemViewModel>();

            var productIds = orderItems.Select(i => i.ProductId).Distinct().ToList();
            var products = await _productService.GetProductsByIdsAsync(productIds);
            var productMap = products.ToDictionary(p => p.Id);

            return orderItems.Select(item =>
            {
                productMap.TryGetValue(item.ProductId, out var product);
                return new CustomerOrderItemViewModel
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    ProductImage = product?.ProductImagePath?.FirstOrDefault() ?? "placeholder.jpg",
                    ProductName = product?.Name ?? "Unknown",
                    ProductDescription = product?.Description ?? string.Empty,
                    TotalPrice = (product?.Price ?? 0) * item.Quantity
                };
            }).ToList();
        }

        public async Task<IEnumerable<SellerOrderItemViewModel>> OrderSellerDetails(string userId)
        {
            var orderItems = (await _orderItemsRepository.GetOrderItemsByUserIdAsync(userId)).ToList();
            if (!orderItems.Any()) return Enumerable.Empty<SellerOrderItemViewModel>();

            var productIds = orderItems.Select(i => i.ProductId).Distinct().ToList();
            var orderIds = orderItems.Select(i => i.OrderId).Distinct().ToList();
            var products = await _productService.GetProductsByIdsAsync(productIds);
            var orders = await _orderRepository.GetOrdersByIdsAsync(orderIds);
            var productMap = products.ToDictionary(p => p.Id);
            var orderMap = orders.ToDictionary(o => o.Id);
            var user = await _userManager.FindByIdAsync(userId);

            return orderItems.Select(item =>
            {
                productMap.TryGetValue(item.ProductId, out var product);
                orderMap.TryGetValue(item.OrderId, out var order);
                return new SellerOrderItemViewModel
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    ProductImage = product?.ProductImagePath?.FirstOrDefault() ?? "placeholder.jpg",
                    ProductName = product?.Name ?? "Unknown",
                    ProductDescription = product?.Description ?? string.Empty,
                    TotalPrice = (product?.Price ?? 0) * item.Quantity,
                    userCity = user?.City,
                    userCountry = user?.Country,
                    userStreet = user?.Street,
                    userPostalCode = user?.PostalCode,
                    userOrderStatus = order?.Status ?? default
                };
            }).ToList();
        }

        public async Task<IEnumerable<AdminOrderItemViewModel>> OrderAdminDetails()
        {
            var orderItems = (await _orderItemsRepository.GetAllOrderItemsAsync()).ToList();
            if (!orderItems.Any()) return Enumerable.Empty<AdminOrderItemViewModel>();

            var productIds = orderItems.Select(i => i.ProductId).Distinct().ToList();
            var orderIds = orderItems.Select(i => i.OrderId).Distinct().ToList();
            var products = await _productService.GetProductsByIdsAsync(productIds);
            var orders = await _orderRepository.GetOrdersByIdsAsync(orderIds);
            var productMap = products.ToDictionary(p => p.Id);
            var orderMap = orders.ToDictionary(o => o.Id);

            return orderItems.Select(item =>
            {
                productMap.TryGetValue(item.ProductId, out var product);
                orderMap.TryGetValue(item.OrderId, out var order);
                return new AdminOrderItemViewModel
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    ProductImage = product?.ProductImagePath?.FirstOrDefault() ?? "placeholder.jpg",
                    ProductName = product?.Name ?? "Unknown",
                    ProductDescription = product?.Description ?? string.Empty,
                    TotalPrice = (product?.Price ?? 0) * item.Quantity,
                    userOrderStatus = order?.Status ?? default
                };
            }).ToList();
        }
    }
}