using EcomGalaxy.ApplicationLayer.Services.IServices;
using EcomGalaxy.Domain.Models.Product;
using EcomGalaxy.Domain.Models.User;
using EcomGalaxy.ViewModel.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcomGalaxy.Controllers.Seller
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IReviewService _reviewService;
        private readonly UserManager<ApplicationUser> _userManager;

        private const int DefaultPageSize = 10;

        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            IReviewService reviewService,
            UserManager<ApplicationUser> userManager)
        {
            _productService = productService;
            _categoryService = categoryService;
            _reviewService = reviewService;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index([FromQuery] ProductQueryParams q)
        {
            q.PageSize = DefaultPageSize;   
            var result = await _productService.GetPagedProductsAsync(q);
            return View(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ProductDetails(int id)
        {
            var userId = _userManager.GetUserId(User);
            var viewModel = await _productService.ProductDetails(id, userId);
            return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductByCategory(string categoryTitle)
        {
            ViewBag.CategoryName = categoryTitle;
            var products = await _productService.GetProductsByCategoryNameAsync(categoryTitle);
            return View(products);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SearchProduct(string searchText)
        {
            var products = await _productService.SearchProductsAsync(searchText);
            return Json(products);
        }


        [HttpGet]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> ProductsForSeller([FromQuery] ProductQueryParams q)
        {
            q.PageSize = DefaultPageSize;
            q.SellerId = User.FindFirstValue(ClaimTypes.NameIdentifier);  // scope to this seller
            var result = await _productService.GetPagedProductsAsync(q);
            return View(result);
        }

        [HttpGet]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> AddProduct()
        {
            ViewData["CategoriesList"] = await _categoryService.GetAllCategoriesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> SaveProduct(AddProductViewModel productVM)
        {
            if (!ModelState.IsValid)
                return View("AddProduct", productVM);

            var sellerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _productService.AddProductAsync(productVM, sellerId);
            return RedirectToAction(nameof(ProductsForSeller));
        }


        [HttpGet]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> ManageProducts([FromQuery] ProductQueryParams q)
        {
            q.PageSize = DefaultPageSize;
            var result = await _productService.GetPagedProductsAsync(q);
            return View(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> ManageProductDetails(int id)
        {
            var sellerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null) return NotFound();

            var isAdmin = User.IsInRole("Admin");
            if (product.ApplicationUserId != sellerId && !isAdmin)
                return Forbid();

            return View(product);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();

            ViewData["CategoriesList"] = await _categoryService.GetAllCategoriesAsync();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> SaveUpdateProduct(int id, Product prd, List<IFormFile> newImages)
        {
            if (newImages.Any())
            {
                var imageFiles = new List<string>();
                foreach (var image in newImages)
                {
                    var fileName = Path.GetFileName(image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    await image.CopyToAsync(stream);
                    imageFiles.Add(fileName);
                }
                prd.ProductImagePath = imageFiles;
            }
            else
            {
                var existing = await _productService.GetProductByIdAsync(id);
                prd.ProductImagePath = existing?.ProductImagePath ?? new List<string>();
            }

            await _productService.UpdateProductAsync(id, prd);
            return RedirectToAction(nameof(ProductsForSeller));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var sellerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var deleted = await _productService.DeleteProductAsync(id, sellerId);

            if (!deleted) return Forbid();
            return RedirectToAction(nameof(ProductsForSeller));
        }
    }
}