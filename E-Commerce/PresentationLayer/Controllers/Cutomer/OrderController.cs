using EcomGalaxy.ApplicationLayer.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcomGalaxy.Controllers.Customer
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AllOrders() => View();

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminOrders()
        {
            var viewModel = await _orderService.OrderAdminDetails();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ReceivedOrder(int orderId)
        {
            await _orderService.ReceivedOrderAsync(orderId);
            return RedirectToAction(nameof(AdminOrders));
        }


        [HttpGet]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> SellerOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = await _orderService.OrderSellerDetails(userId);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> ShipOrder(int orderId)
        {
            await _orderService.ShipOrderAsync(orderId);
            return RedirectToAction(nameof(SellerOrders));
        }


        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CustomerOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = await _orderService.CustomerOrders(userId);
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Customer,Seller,Admin")]
        public async Task<IActionResult> OrderDetails(int orderId)
        {
            if (User.IsInRole("Customer"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var order = await _orderService.GetOrderByIdAsync(orderId);
                if (order == null || order.CustomerId != userId)
                    return Forbid();
            }

            var viewModel = await _orderService.OrderDetails(orderId);
            ViewBag.OrderId = orderId;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null || order.CustomerId != userId)
                return Forbid();

            await _orderService.CancelOrderAsync(orderId);
            return RedirectToAction(nameof(CustomerOrders));
        }
    }
}