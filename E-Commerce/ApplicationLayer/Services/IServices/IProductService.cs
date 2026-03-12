using EcomGalaxy.Domain.Models.Product;
using EcomGalaxy.ViewModel;
using EcomGalaxy.ViewModel.Product;

namespace EcomGalaxy.ApplicationLayer.Services.IServices
{
    public interface IProductService
    {
        // Write
        Task AddProductAsync(AddProductViewModel productVM, string sellerId);
        Task UpdateProductAsync(int productId, Product product);
        Task UpdateRangeAsync(IEnumerable<Product> products);
        Task<bool> DeleteProductAsync(int productId, string sellerId);

        // Read — single
        Task<Product?> GetProductByIdAsync(int productId);
        Task<Product?> GetProductByNameAsync(string productName);

        // Read — collections
        Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetProductsByIdsAsync(IEnumerable<int> productIds);
        Task<IEnumerable<ProductViewModel>> GetProductsBySellerIdAsync(string sellerId);
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task<IEnumerable<ProductViewModel>> GetProductsByCategoryNameAsync(string categoryName);
        Task<IEnumerable<ProductViewModel>> SearchProductsAsync(string searchText);

        /// <summary>
        /// Unified paged browse — search, sort, filter, and pagination in one call.
        /// </summary>
        Task<PagedResult<ProductViewModel>> GetPagedProductsAsync(ProductQueryParams q);

        // Sort & filter (kept for non-browse use-cases)
        Task<IEnumerable<Product>> SortProductsDescending();
        Task<IEnumerable<Product>> SortProductsAscending();
        Task<IEnumerable<Product>> FilterProductsByAverageRating(int averageRating);
        Task<IEnumerable<Product>> FilterProductsByPrice(int from, int to);

        // Projections
        Task<ProductDetailsFormViewModel> ProductDetails(int productId, string userId);
    }
}