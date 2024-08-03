
using EcomGalaxy.Models;
using EcomGalaxy.Services.IServices;

namespace EcomGalaxy.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool?> AddCategoryAsync(Category newCategory)
        {
            var Cate = await _categoryRepository.GetCategoryByNameAsync(newCategory.Name);
            
            if (Cate != null)  return false;

            return await _categoryRepository.AddCategoryAsync(newCategory);
        }

        public async Task<bool?> DeleteCategoryAsync(int categoryId)
        {
            return await _categoryRepository.DeleteCategoryAsync(categoryId);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _categoryRepository.GetCategoryByIdAsync(categoryId);
        }

        public async Task<Category> GetCategoryByNameAsync(string categoryName)
        {
            return await _categoryRepository.GetCategoryByNameAsync(categoryName);
        }

        public async Task<bool?> UpdateCategoryAsync(string categoryName, Category categoryToUpdate)
        {
            var cat = await _categoryRepository.GetCategoryByNameAsync(categoryName);

            if (cat != null)
            {
                return false;
            }
            else
            {
                await _categoryRepository.UpdateCategoryAsync(categoryToUpdate.Id, categoryToUpdate);
                return true;
            }
        }


    }
}
