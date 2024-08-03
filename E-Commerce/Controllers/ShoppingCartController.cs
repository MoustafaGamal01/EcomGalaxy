namespace EcomGalaxy.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet] // To Be
        public IActionResult ViewCart()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToCart()
        {
            await _shoppingCartService.AddShoppingCartAsync();
            return View();
        }
    }
}
