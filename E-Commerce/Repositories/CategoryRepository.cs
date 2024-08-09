using EcomGalaxy.Models.Context;

namespace EcomGalaxy.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyContext _context;

        public CategoryRepository(MyContext context)
        {
            this._context = context;
        }

        public async Task<bool?> AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool?> DeleteCategoryAsync(int categoryId)
        {
            var existingCategory = await GetCategoryByIdAsync(categoryId);
            if (existingCategory == null)
            {
                throw new InvalidOperationException($"Category with ID {categoryId} not found.");
            }
            _context.Categories.Remove(existingCategory);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId); 
        }

        public Task<Category> GetCategoryByNameAsync(string categoryName)
        {
            return _context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
        }

        public async Task<bool?> UpdateCategoryAsync(int categoryId, Category category)
        {
            var existingCategory = await GetCategoryByIdAsync(categoryId);
            if (existingCategory == null)
            {
                throw new InvalidOperationException($"Category with ID {categoryId} not found.");
            }
            _context.Entry(existingCategory).CurrentValues.SetValues(category);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
