using EcomGalaxy.Models.Product;

namespace EcomGalaxy.Repositories.IRepository
{
    public interface IProductRepository
    {
        Task<bool?> AddProductAsync(Product product);

        Task<bool?> UpdateProductAsync(int productId, Product product);

        Task<bool?> DeleteProductAsync(int productId);
        
        Task<IEnumerable<Product>> GetAllProductsAsync();
        
        Task<Product> GetProductByIdAsync(int productId);

        Task<IEnumerable<Product>> SearchForAProduct(string productName);

        Task<IEnumerable<Product>> GetProductsBySellerIdAsync(string SellerId);

        Task<Product> GetProductByNameAsync(string productName);

        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);

        Task<IEnumerable<Product>> GetProductsByCategoryNameAsync(string categoryName);

        Task<IEnumerable<Product>> SortProductsDescending();

        Task<IEnumerable<Product>> SortProductsAscending();

        Task<IEnumerable<Product>> FilterProductsByAverageRating(int AverageRatine);

        Task<IEnumerable<Product>> FilterProductsByPrice(int from, int to);

    }
}
