namespace EcomGalaxy.Services.IServices
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task<Category> GetCategoryByNameAsync(string categoryName);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<bool?> AddCategoryAsync(Category newCategory);
        Task<bool?> UpdateCategoryAsync(string categoryName, Category categoryToUpdate);
        Task<bool?> DeleteCategoryAsync(int categoryId);
    }
}
