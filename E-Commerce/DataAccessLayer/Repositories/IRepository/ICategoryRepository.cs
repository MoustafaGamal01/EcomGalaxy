using EcomGalaxy.Domain.Models.Product;

namespace EcomGalaxy.DataAccess.Repositories.IRepository
{
    public interface ICategoryRepository
    {
        Task<bool?> AddCategoryAsync(Category category);

        Task<bool?> UpdateCategoryAsync(int categoryId, Category category);

        Task<bool?> DeleteCategoryAsync(int categoryId);

        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        Task<Category> GetCategoryByIdAsync(int categoryId);

        Task<Category> GetCategoryByNameAsync(string categoryName);
    }
}
