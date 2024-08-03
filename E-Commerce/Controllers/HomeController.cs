

namespace EcomGalaxy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryRepository _categoryService;

        public HomeController(ILogger<HomeController> logger, ICategoryRepository categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
