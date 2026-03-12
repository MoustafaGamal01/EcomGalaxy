using EcomGalaxy.ApplicationLayer.Services.IServices;
using EcomGalaxy.DataAccess.Repositories.IRepository;
using EcomGalaxy.Domain.Models.Product;
using EcomGalaxy.Domain.Models.User;
using EcomGalaxy.ViewModel;
using EcomGalaxy.ViewModel.Product;
using EcomGalaxy.ViewModel.Review;
using Microsoft.AspNetCore.Identity;

namespace EcomGalaxy.ApplicationLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IReviewService _reviewService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductService(
            IProductRepository productRepository,
            IReviewService reviewService,
            UserManager<ApplicationUser> userManager)
        {
            _productRepository = productRepository;
            _reviewService = reviewService;
            _userManager = userManager;
        }

        public async Task AddProductAsync(AddProductViewModel productVM, string sellerId)
        {
            var product = new Product
            {
                Name = productVM.Name,
                Description = productVM.Description,
                Price = productVM.Price,
                StockQuantity = productVM.StockQuantity,
                ProductImagePath = productVM.ProductImagePath,
                CategoryId = productVM.CategoryId,
                ApplicationUserId = sellerId,
                AverageRating = 0
            };
            await _productRepository.AddProductAsync(product);
        }

        public async Task UpdateProductAsync(int productId, Product product)
            => await _productRepository.UpdateProductAsync(productId, product);

        public async Task UpdateRangeAsync(IEnumerable<Product> products)
            => await _productRepository.UpdateRangeAsync(products);

        public async Task<bool> DeleteProductAsync(int productId, string sellerId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null || product.ApplicationUserId != sellerId) return false;
            await _productRepository.DeleteProductAsync(productId);
            return true;
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
            => await _productRepository.GetProductByIdAsync(productId);

        public async Task<Product?> GetProductByNameAsync(string productName)
            => await _productRepository.GetProductByNameAsync(productName);

        public async Task<IEnumerable<Product>> GetProductsByIdsAsync(IEnumerable<int> productIds)
            => await _productRepository.GetProductsByIdsAsync(productIds);

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
            => await _productRepository.GetProductsByCategoryIdAsync(categoryId);

        public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
            => ToViewModels(await _productRepository.GetAllProductsAsync());

        public async Task<IEnumerable<ProductViewModel>> GetProductsBySellerIdAsync(string sellerId)
            => ToViewModels(await _productRepository.GetProductsBySellerIdAsync(sellerId));

        public async Task<IEnumerable<ProductViewModel>> GetProductsByCategoryNameAsync(string categoryName)
            => ToViewModels(await _productRepository.GetProductsByCategoryNameAsync(categoryName));

        public async Task<IEnumerable<ProductViewModel>> SearchProductsAsync(string searchText)
        {
            var products = string.IsNullOrEmpty(searchText)
                ? await _productRepository.GetAllProductsAsync()
                : await _productRepository.SearchForAProduct(searchText);
            return ToViewModels(products);
        }

        public async Task<PagedResult<ProductViewModel>> GetPagedProductsAsync(ProductQueryParams q)
        {
            // Guard against bad query string values
            q.Page = Math.Max(1, q.Page);
            q.PageSize = Math.Clamp(q.PageSize, 1, 50);

            var (items, totalCount) = await _productRepository.GetPagedProductsAsync(q);

            return new PagedResult<ProductViewModel>
            {
                Items = ToViewModels(items),
                CurrentPage = q.Page,
                PageSize = q.PageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)q.PageSize)
            };
        }

        public async Task<IEnumerable<Product>> SortProductsDescending()
            => await _productRepository.SortProductsDescending();

        public async Task<IEnumerable<Product>> SortProductsAscending()
            => await _productRepository.SortProductsAscending();

        public async Task<IEnumerable<Product>> FilterProductsByAverageRating(int averageRating)
            => await _productRepository.FilterProductsByAverageRating(averageRating);

        public async Task<IEnumerable<Product>> FilterProductsByPrice(int from, int to)
            => await _productRepository.FilterProductsByPrice(from, to);

        public async Task<ProductDetailsFormViewModel> ProductDetails(int productId, string userId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId)
                ?? throw new KeyNotFoundException($"Product {productId} not found.");

            var reviews = (await _reviewService.GetReviewsByProductIdAsync(productId)).ToList();

            var userIds = reviews.Select(r => r.ApplicationUserId).Distinct().ToList();
            var users = await _userManager.Users
                              .Where(u => userIds.Contains(u.Id))
                              .ToListAsync();
            var userMap = users.ToDictionary(u => u.Id);

            var reviewsVM = reviews.Select(review =>
            {
                userMap.TryGetValue(review.ApplicationUserId, out var user);
                return new ShowReviewViewModel
                {
                    Message = review.Message,
                    Rating = review.Rating,
                    UserName = user?.Name ?? "Unknown"
                };
            }).ToList();

            return new ProductDetailsFormViewModel
            {
                ProductId = product.Id,
                UserId = userId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Images = product.ProductImagePath,
                Reviews = reviewsVM,
                Rate = product.AverageRating
            };
        }

        private static IEnumerable<ProductViewModel> ToViewModels(IEnumerable<Product> products)
        {
            return products.Select(p => new ProductViewModel
            {
                ProductId = p.Id,
                ProductName = p.Name.Length > 18 ? p.Name[..18] + "..." : p.Name,
                ProductDescription = p.Description.Length > 20 ? p.Description[..20] + "..." : p.Description,
                ProductImages = p.ProductImagePath,
                ProductPrice = p.Price,
                ProductStock = p.StockQuantity,
                CategoryId = p.CategoryId
            });
        }
    }
}