using EcomGalaxy.Models.Context;
using EcomGalaxy.Models.Product;
using System.Reflection.Metadata;

namespace EcomGalaxy.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyContext _context;

        public ProductRepository(MyContext context)
        {
            this._context = context;
        }

        public async Task<bool?> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool?> DeleteProductAsync(int productId)
        {
            var product = await GetProductByIdAsync(productId);
            if (product == null)
            {
                throw new InvalidOperationException($"Product with ID {productId} not found.");
            }
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<IEnumerable<Product>> SearchForAProduct(string searchText)
        {
            return await _context.Products.Include(p=>p.Category).Where(p => 
            (p.Category.Name.Contains(searchText)) || (p.Name.Contains(searchText)) || (p.Description.Contains(searchText)))
                .ToListAsync();
        }

        public async Task<Product> GetProductByNameAsync(string productName)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Name == productName);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _context.Products.Include(c => c.Category)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryNameAsync(string categoryName)
        {
            return await _context.Products.Include(c => c.Category)
                .Where(p => p.Category.Name == categoryName)
                .ToListAsync();
        }

        public async Task<bool?> UpdateProductAsync(int productId, Product product)
        {
            var existingProdcut = await GetProductByIdAsync(productId);
            if (existingProdcut == null)
            {
                throw new InvalidOperationException($"Product with ID {productId} not found.");
            }
            _context.Entry(existingProdcut).CurrentValues.SetValues(product);
            Console.WriteLine(product.ApplicationUserId);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Product>> GetProductsBySellerIdAsync(string SellerId)
        {
            return await _context.Products
                .Include(u => u.ApplicationUser)
                .Where(p => p.ApplicationUserId == SellerId).ToListAsync();
        }

        public async Task<IEnumerable<Product>> SortProductsDescending()
        {
            return await _context.Products
                .OrderByDescending(p=>p.Price)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> SortProductsAscending()
        {
            return await _context.Products
                .OrderBy(p => p.Price)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> FilterProductsByAverageRating(int AverageRating)
        {
            return await _context.Products
                .Where(p => (p.AverageRating >= AverageRating) && (p.AverageRating<= AverageRating + 1))
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> FilterProductsByPrice(int from, int to)
        {
            return await _context.Products
                .Where(p => (p.Price >= from) && (p.Price <= to))
                .ToListAsync();
        }
    }
}
