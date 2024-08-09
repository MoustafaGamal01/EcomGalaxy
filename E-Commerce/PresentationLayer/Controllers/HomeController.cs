using EcomGalaxy.DataAccess.Repositories.IRepository;
using EcomGalaxy.Domain.Models.Product;

namespace EcomGalaxy.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryRepository _categoryService;

        public HomeController(ILogger<HomeController> logger, ICategoryRepository categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        [HttpGet]
        [AllowAnonymous]
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
            return View(new EcomGalaxy.Domain.Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
