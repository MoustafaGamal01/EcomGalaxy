using EcomGalaxy.Domain.Models.Product;
using EcomGalaxy.ViewModel.Product;

namespace EcomGalaxy.DataAccess.Repositories.IRepository
{
    public interface IProductRepository
    {
        // Write
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(int productId, Product product);
        Task UpdateRangeAsync(IEnumerable<Product> products);
        Task DeleteProductAsync(int productId);

        // Read — single
        Task<Product?> GetProductByIdAsync(int productId);
        Task<Product?> GetProductByNameAsync(string productName);

        // Read — collections
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetProductsByIdsAsync(IEnumerable<int> productIds);
        Task<IEnumerable<Product>> GetProductsBySellerIdAsync(string sellerId);
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Product>> GetProductsByCategoryNameAsync(string categoryName);
        Task<IEnumerable<Product>> SearchForAProduct(string searchText);

      
        Task<(IEnumerable<Product> Items, int TotalCount)> GetPagedProductsAsync(ProductQueryParams q);

        Task<IEnumerable<Product>> SortProductsDescending();
        Task<IEnumerable<Product>> SortProductsAscending();
        Task<IEnumerable<Product>> FilterProductsByAverageRating(int averageRating);
        Task<IEnumerable<Product>> FilterProductsByPrice(int from, int to);
    }
}