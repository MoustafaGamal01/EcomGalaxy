using EcomGalaxy.Domain.Models.ShoppingCart;

namespace EcomGalaxy.DataAccess.Repositories.IRepository
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetShoppingCartByUserIdAsync(string userId);
        Task AddShoppingCartAsync(ShoppingCart cart);
        Task AddShoppingCartItemAsync(ShoppingCartItem item);
        Task<ShoppingCartItem> GetShoppingCartItemAsync(int cartId, int productId);
        Task RemoveShoppingCartItemAsync(ShoppingCartItem item);
        Task ClearCartAsync(int cartId);
        Task SaveChangesAsync();
    }
}
