using EcomGalaxy.DataAccess.Repositories.IRepository;
using EcomGalaxy.Domain.Models.Context;
using EcomGalaxy.Domain.Models.Product;
using EcomGalaxy.ViewModel.Product;
using Microsoft.EntityFrameworkCore;

namespace EcomGalaxy.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyContext _context;

        public ProductRepository(MyContext context)
        {
            _context = context;
        }


        public async Task AddProductAsync(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(int productId, Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            var existing = await _context.Products.FindAsync(productId)
                ?? throw new KeyNotFoundException($"Product {productId} not found.");

            product.ApplicationUserId = existing.ApplicationUserId;
            _context.Entry(existing).CurrentValues.SetValues(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<Product> products)
        {
            if (products == null) throw new ArgumentNullException(nameof(products));
            _context.Products.UpdateRange(products);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int productId)
        {
            var existing = await _context.Products.FindAsync(productId)
                ?? throw new KeyNotFoundException($"Product {productId} not found.");

            _context.Products.Remove(existing);
            await _context.SaveChangesAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
            => await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == productId);

        public async Task<Product?> GetProductByNameAsync(string productName)
            => await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Name == productName);


        public async Task<IEnumerable<Product>> GetAllProductsAsync()
            => await _context.Products.AsNoTracking().ToListAsync();

        public async Task<IEnumerable<Product>> GetProductsByIdsAsync(IEnumerable<int> productIds)
        {
            if (productIds == null || !productIds.Any()) return Enumerable.Empty<Product>();
            return await _context.Products.AsNoTracking()
                .Where(p => productIds.Contains(p.Id)).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsBySellerIdAsync(string sellerId)
        {
            if (string.IsNullOrEmpty(sellerId)) throw new ArgumentNullException(nameof(sellerId));
            return await _context.Products.AsNoTracking()
                .Where(p => p.ApplicationUserId == sellerId).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
            => await _context.Products.AsNoTracking().Where(p => p.CategoryId == categoryId).ToListAsync();

        public async Task<IEnumerable<Product>> GetProductsByCategoryNameAsync(string categoryName)
            => await _context.Products.AsNoTracking()
                .Include(p => p.Category)
                .Where(p => p.Category.Name == categoryName).ToListAsync();

        public async Task<IEnumerable<Product>> SearchForAProduct(string searchText)
            => await _context.Products.AsNoTracking()
                .Include(p => p.Category)
                .Where(p => p.Name.Contains(searchText) ||
                            p.Description.Contains(searchText) ||
                            p.Category.Name.Contains(searchText))
                .ToListAsync();

        public async Task<(IEnumerable<Product> Items, int TotalCount)> GetPagedProductsAsync(ProductQueryParams q)
        {
            IQueryable<Product> query = _context.Products.AsNoTracking();

            if (!string.IsNullOrEmpty(q.SellerId))
                query = query.Where(p => p.ApplicationUserId == q.SellerId);


            if (!string.IsNullOrWhiteSpace(q.Search))
            {
                var s = q.Search.Trim();
                query = query
                    .Include(p => p.Category)
                    .Where(p => p.Name.Contains(s) ||
                                p.Description.Contains(s) ||
                                p.Category.Name.Contains(s));
            }

            if (q.MinPrice.HasValue)
                query = query.Where(p => p.Price >= (double)q.MinPrice.Value);

            if (q.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= (double)q.MaxPrice.Value);

            if (q.MinRating.HasValue)
                query = query.Where(p => p.AverageRating >= q.MinRating.Value);

            // ── Sort ──────────────────────────────────────────────────────────────

            query = q.Sort switch
            {
                "price_asc" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                "rating_desc" => query.OrderByDescending(p => p.AverageRating),
                _ => query.OrderBy(p => p.Id)   // stable default
            };

            // ── Paginate ──────────────────────────────────────────────────────────

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((q.Page - 1) * q.PageSize)
                .Take(q.PageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        // ── Legacy sort & filter (kept for admin / non-browse use) ────────────────

        public async Task<IEnumerable<Product>> SortProductsDescending()
            => await _context.Products.AsNoTracking().OrderByDescending(p => p.Price).ToListAsync();

        public async Task<IEnumerable<Product>> SortProductsAscending()
            => await _context.Products.AsNoTracking().OrderBy(p => p.Price).ToListAsync();

        public async Task<IEnumerable<Product>> FilterProductsByAverageRating(int averageRating)
            => await _context.Products.AsNoTracking()
                .Where(p => p.AverageRating >= averageRating && p.AverageRating < averageRating + 1)
                .ToListAsync();

        public async Task<IEnumerable<Product>> FilterProductsByPrice(int from, int to)
            => await _context.Products.AsNoTracking()
                .Where(p => p.Price >= from && p.Price <= to).ToListAsync();
    }
}