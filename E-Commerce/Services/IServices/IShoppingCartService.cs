using EcomGalaxy.Models.ShoppingCart;
using EcomGalaxy.ViewModel.Order;

namespace EcomGalaxy.Services.IServices
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetOrCreateShoppingCartAsync(string userId);
        Task AddToCartAsync(string userId, int productId);
        Task RemoveFromCartAsync(int cartItemId, int cartId);
        Task UpdateCartItemQuantityAsync(int cartItemId, int cartId, int quantity);
        Task ClearShoppingCart(string userId);
        Task Checkout(string userId, OrderCheckOutViewModel orderVM);
    }
}
