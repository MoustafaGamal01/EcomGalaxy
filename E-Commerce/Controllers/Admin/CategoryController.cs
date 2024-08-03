using Microsoft.AspNetCore.Mvc;

namespace EcomGalaxy.Controllers.Admin
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryRepository)
        {
            _categoryService = categoryRepository;
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                var ok = await _categoryService.AddCategoryAsync(category);

                if (ok == true)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Content("Category Exists");
                }
			}
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> ManageCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryDetails(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveUpdateCategory(Category category, IFormFile newImage)
        {
            if (newImage != null)
            {
                var fileName = Path.GetFileName(newImage.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await newImage.CopyToAsync(stream);
                }
                category.CategoryImage = fileName;
            }
            else
            {
                var oldCategory = await _categoryService.GetCategoryByIdAsync(category.Id);
                category.CategoryImage = oldCategory.CategoryImage;
            }

            var ok = await _categoryService.UpdateCategoryAsync(category.Name, category);
            if (ok == true) return RedirectToAction("UpdateCategory", new { id = category.Id });
            
            return  Json("Category Exists");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            bool?ok = await _categoryService.DeleteCategoryAsync(id);
            if (ok == true)
                return RedirectToAction("ManageCategories");
            return Json("Category Not Deleted");
        }
    }
}
