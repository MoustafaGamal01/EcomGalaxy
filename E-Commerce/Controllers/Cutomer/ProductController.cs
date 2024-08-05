using EcomGalaxy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using System.Security.Claims;

namespace EcomGalaxy.Controllers.Cutomer
{
    public class ProductController : Controller
    {
        private readonly MyContext context;
        private readonly IProductService _productService;
        
        private readonly ICategoryService _categoryService;

        public ProductController(MyContext context, IProductService productService, ICategoryService categoryService)
        {
            this.context = context;
            _productService = productService;
            _categoryService = categoryService;
        }
        
        public async Task<IActionResult> Index()
        {
            var prdVms =  await _productService.GetAllProductsAsync();
            return View(prdVms);
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            ViewData["CategoriesList"] = await _categoryService.GetAllCategoriesAsync();
            return View("AddProduct");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveProduct(AddProductViewModel productVM)
        {
            string sellerId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            
            if (ModelState.IsValid)
            {
                await _productService.AddProductAsync(productVM, sellerId);

                return RedirectToAction("ProductsForSeller", "Product");
            }
            
            return Content("ModelIsNotValid");
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetails(int id)
        {
            Product product = await _productService.GetProductByIdAsync(id);
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            string sellerId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            bool? ok = await _productService.DeleteProductAsync(id, sellerId);
           
            if (ok == true)
            {
                return RedirectToAction("ProductsForSeller");
            }
            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            Product product = await _productService.GetProductByIdAsync(id);
            ViewData["CategoriesList"] = await _categoryService.GetAllCategoriesAsync();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveUpdateProduct(int id, Product prd, List<IFormFile> newImages)
        {
            var imageFiles = new List<string>();
            if (newImages.Count != 0)
            {
                foreach (var newImage in newImages)
                {
                    var fileName = Path.GetFileName(newImage.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await newImage.CopyToAsync(stream);
                    }

                    imageFiles.Add(fileName);
                }

                prd.ProductImagePath = imageFiles;
            }
            else
            {
                var oldPrd = await _productService.GetProductByIdAsync(id);
                prd.ProductImagePath = oldPrd.ProductImagePath;
            }

            await _productService.UpdateProductAsync(id, prd);
            return RedirectToAction("ManageProductDetails", new { id = prd.Id });
        }

        [HttpGet]
        public async Task<IActionResult> SearchProduct(string searchText)
        {
            var prds = await _productService.SearchProductsAsync(searchText);

            return Json(prds);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductByCategory(string categoryTitle)
        {
            var prdVms = await _productService.GetProductsByCategoryNameAsync(categoryTitle);

            return View(prdVms);
        }

        [HttpGet]
        public async Task<IActionResult> ManageProducts()
        {
            var prds = await _productService.GetAllProductsAsync();

            return View(prds);
        }

        [HttpGet]
        // This method must be Post So update it
        public async Task<IActionResult> ManageProductDetails(int id)
        {
            var prd = await _productService.GetProductByIdAsync(id);
            string sellerId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if(prd.ApplicationUserId == sellerId)
            {
                return View(prd);
            }
            return Unauthorized();
        }

        [HttpGet]
        public async Task<IActionResult> ProductsForSeller()
        {
            string sellerId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var prdVms = await _productService.GetProductsBySellerIdAsync(sellerId);

            return View(prdVms);
        }
    }
}
