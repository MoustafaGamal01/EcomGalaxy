﻿using EcomGalaxy.ApplicationLayer.Services.IServices;
using EcomGalaxy.Domain.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace EcomGalaxy.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
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
            bool changeName = false;
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
                if(oldCategory.Name != category.Name)
                    changeName = true;
            }

            var ok = await _categoryService.UpdateCategoryAsync(category.Name, category, changeName);
            if (ok == true) return RedirectToAction("UpdateCategory", new { id = category.Id });
            
            
            ModelState.AddModelError("", "Category Exists");
            return View("UpdateCategory", new { id = category.Id});
        }

       [HttpGet]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            bool?ok = await _categoryService.DeleteCategoryAsync(id);
            if (ok == true)
                return RedirectToAction("ManageCategories");
            ModelState.AddModelError("", "Can't delete category");
            return RedirectToAction("ManageCategories");
        }
    }
}
