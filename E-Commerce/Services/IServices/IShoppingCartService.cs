namespace EcomGalaxy.Services.IServices
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetOrCreateShoppingCartAsync(string userId);
        Task AddToCartAsync(string userId, int productId/*, int quantity*/);
        Task RemoveFromCartAsync(int cartItemId, int cartId);
        Task UpdateCartItemQuantityAsync(int cartItemId, int cartId, int quantity);
    }
}
