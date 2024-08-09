using EcomGalaxy.ApplicationLayer.Services.IServices;
using EcomGalaxy.Domain.Models.Payment;
using EcomGalaxy.Domain.Models.User;
using EcomGalaxy.ViewModel.Order;

namespace EcomGalaxy.Controllers.Cutomer
{
    [Authorize(Roles = "Customer")]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingCartController(IShoppingCartService shoppingCartService,
            UserManager<ApplicationUser> userManager)
        {
            _shoppingCartService = shoppingCartService;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ViewCart()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var shoppingCart = await _shoppingCartService.GetOrCreateShoppingCartAsync(userId);

            return View(shoppingCart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            await _shoppingCartService.AddToCartAsync(userId, productId);

            TempData["Notification"] = "Item added to cart!";
            return RedirectToAction("ViewCart");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(int productId, int cartId)
        {
            await _shoppingCartService.RemoveFromCartAsync(productId, cartId);
            return RedirectToAction("ViewCart");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateQuantity(int productId, int cartId, int quantity)
        {
            await _shoppingCartService.UpdateCartItemQuantityAsync(productId, cartId, quantity);
            return RedirectToAction("ViewCart");
        }


        [HttpGet]
        public IActionResult CheckoutForm()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(OrderCheckOutViewModel orderVM)
        {
            if (orderVM.PaymentStatus == PaymentStatus.CashOnDelivery)
            {
                var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                await _shoppingCartService.Checkout(user, orderVM);
            }
            else if (ModelState.IsValid)
            {
                var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                await _shoppingCartService.Checkout(user, orderVM);
            }
            return RedirectToAction("CustomerOrders", "Order");
        }

    }
}
