using EcomGalaxy.Models;
using EcomGalaxy.Services.IServices;

namespace EcomGalaxy.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        private List<ProductViewModel> FromProductToProductVM(List<ProductViewModel> productsVm, IEnumerable<Product> products)
        {
            var prdVMs = products.Select(p => new ProductViewModel
            {
                ProductId = p.Id,
                ProductName = p.Name.Length > 18 ? p.Name.Substring(0, 18) + "..." : p.Name,
                ProductDescription = p.Description.Length > 20 ? p.Description.Substring(0, 20) + "..." : p.Description,
                ProductImages = p.ProductImagePath,
                ProductPrice = p.Price,
                ProductStock = p.StockQuantity,
                CategoryId = p.CategoryId
            }).ToList();

            return prdVMs;
        }

        public async Task<bool?> AddProductAsync(AddProductViewModel productVM, string sellerId)
        {
            Product newProduct = new Product();
            newProduct.CategoryId = productVM.CategoryId;
            newProduct.Description = productVM.Description;
            newProduct.StockQuantity = productVM.StockQuantity;
            newProduct.ProductImagePath = productVM.ProductImagePath;
            newProduct.Name = productVM.Name;
            newProduct.Price = productVM.Price;
            newProduct.AverageRating = 0;
            newProduct.ApplicationUserId = sellerId;

            return await _productRepository.AddProductAsync(newProduct);    
        }

        public async Task<bool?> DeleteProductAsync(int productId, string sellerId)
        {
            var prd = await _productRepository.GetProductByIdAsync(productId);
            if(sellerId == prd.ApplicationUserId)
            {
                await _productRepository.DeleteProductAsync(productId);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
        {
            IEnumerable<Product> prdsForSeller = await _productRepository.GetAllProductsAsync();
            List<ProductViewModel> prdVms = new List<ProductViewModel>();

            return FromProductToProductVM(prdVms, prdsForSeller);
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _productRepository.GetProductByIdAsync(productId);
        }

        public async Task<Product> GetProductByNameAsync(string productName)
        {
            return await _productRepository.GetProductByNameAsync(productName);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _productRepository.GetProductsByCategoryIdAsync(categoryId);
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsByCategoryNameAsync(string categoryName)
        {
            IEnumerable<Product> products = await _productRepository.GetProductsByCategoryNameAsync(categoryName);
            List<ProductViewModel> prdVms = new List<ProductViewModel>();

            return FromProductToProductVM(prdVms, products);
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsBySellerIdAsync(string sellerId)
        {
            IEnumerable<Product> products = await _productRepository.GetProductsBySellerIdAsync(sellerId);
            List<ProductViewModel> prdVms = new List<ProductViewModel>();
            
            return FromProductToProductVM(prdVms, products);
        }

        public async Task<IEnumerable<ProductViewModel>> SearchProductsAsync(string searchText)
        {
            IEnumerable<Product> products;
            List<ProductViewModel> prdVms = new List<ProductViewModel>();

            if (string.IsNullOrEmpty(searchText)) products = await _productRepository.GetAllProductsAsync();
            else products = await _productRepository.SearchForAProduct(searchText);


            return FromProductToProductVM(prdVms, products);
        }

        public async Task<bool?> UpdateProductAsync(int productId, Product product)
        {
            // handle appuserid in updateing => 
            var existPrd = await _productRepository.GetProductByIdAsync(productId);
            product.ApplicationUserId = existPrd.ApplicationUserId; 
            return await _productRepository.UpdateProductAsync(productId, product);
        }

        public async Task<IEnumerable<Product>> SearchForAProduct(string searchText)
        {
            return await _productRepository.SearchForAProduct(searchText);  
        }

        public async Task<IEnumerable<Product>> SortProductsDescending()
        {
            return await _productRepository.SortProductsDescending();
        }

        public async Task<IEnumerable<Product>> SortProductsAscending()
        {
            return await _productRepository.SortProductsAscending();
        }

        public async Task<IEnumerable<Product>> FilterProductsByAverageRating(int AverageRating)
        {
            return await _productRepository.FilterProductsByAverageRating(AverageRating);
        }

        public async Task<IEnumerable<Product>> FilterProductsByPrice(int from, int to)
        {
            return await _productRepository.FilterProductsByPrice(from, to);
        }
    }
}
