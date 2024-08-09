using System.Collections.Generic;
using System.Threading.Tasks;
using EcomGalaxy.Domain.Models.Product;
using EcomGalaxy.ViewModel.Product;

namespace EcomGalaxy.ApplicationLayer.Services.IServices
{
    public interface IProductService
    {
        Task<bool?> AddProductAsync(AddProductViewModel product, string sellerId);

        Task<bool?> UpdateProductAsync(int productId, Product product);

        Task<bool?> DeleteProductAsync(int productId, string sellerId);

        Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();

        Task<Product> GetProductByIdAsync(int productId);

        Task<IEnumerable<ProductViewModel>> SearchProductsAsync(string searchText);

        Task<Product> GetProductByNameAsync(string productName);

        Task<IEnumerable<Product>> SearchForAProduct(string productName);

        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);

        Task<IEnumerable<ProductViewModel>> GetProductsByCategoryNameAsync(string categoryName);

        Task<IEnumerable<ProductViewModel>> GetProductsBySellerIdAsync(string SellerId);


        Task<IEnumerable<Product>> SortProductsDescending();

        Task<IEnumerable<Product>> SortProductsAscending();

        Task<IEnumerable<Product>> FilterProductsByAverageRating(int AverageRatine);

        Task<IEnumerable<Product>> FilterProductsByPrice(int from, int to);

        Task<ProductDetailsFormViewModel> ProductDetails(int productId, string userId);
    }
}
