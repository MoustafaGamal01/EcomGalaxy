using EcomGalaxy.Models;
using EcomGalaxy.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace EcomGalaxy.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemsService _itemsService;
        private readonly IProductService _productService;
        private readonly IPaymentService _paymentService;

        public OrderController(IOrderService orderService, IOrderItemsService orderItemsService,
            IProductService productService, IPaymentService paymentService)
        {
            _orderService = orderService;
            _itemsService = orderItemsService;
            _productService = productService;
            _paymentService = paymentService;
        }

        [HttpGet]
        public IActionResult AllOrders()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CustomerOrders()
        {
            var user = User.Claims.FirstOrDefault(c=>c.Type == ClaimTypes.NameIdentifier).Value;
            var lstordersMainViewModel = await _orderService.CustomerOrders(user);
            return View(lstordersMainViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> SellerOrders()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var lstViewModel = await _orderService.OrderSellerDetails(userId);
            return View(lstViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetails(int orderId)
        {
            var lstViewModel = await _orderService.OrderDetails(orderId);
            ViewBag.OrderId = orderId;
            return View(lstViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var orderItems = await _itemsService.GetOrderItemsByOrderIdAsync(orderId);
            var order = await _orderService.GetOrderByIdAsync(orderId);
            var paymentId = order.PaymentId;
            // clear items=>add qtty to prds && del payment && del order
            foreach (var item in orderItems)
            {
                var product = await _productService.GetProductByIdAsync(item.ProductId);
                product.StockQuantity += item.Quantity;
                await _productService.UpdateProductAsync(product.Id, product);
                await _itemsService.DeleteOrderItemAsync(item.Id);
            }

            await _orderService.DeleteOrderAsync(orderId);

            await _paymentService.DeletePaymentAsync(paymentId);

            return RedirectToAction("CustomerOrders", "Order");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShipOrder(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            order.Status = OrderStatus.Shipped;
            order.ShippedDate = DateTime.Now;
            await _orderService.UpdateOrderAsync(orderId, order);
            return RedirectToAction("SellerOrders", "Order");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RecievedOrder(int orderId)
        {
            var orderItems = await _itemsService.GetOrderItemsByOrderIdAsync(orderId);
            var order = await _orderService.GetOrderByIdAsync(orderId);
            var paymentId = order.PaymentId;

            foreach (var item in orderItems)
            {
                await _itemsService.DeleteOrderItemAsync(item.Id);
            }

            await _orderService.DeleteOrderAsync(orderId);

            await _paymentService.DeletePaymentAsync(paymentId);

            return RedirectToAction("SellerOrders", "Order");
        }

        [HttpGet]
        public async Task<IActionResult> AdminOrders()
        {
            var lstViewModel = await _orderService.OrderAdminDetails();
            return View(lstViewModel);
        }
    }
}
