namespace EcomGalaxy.Services.IServices
{
    public interface IShoppingCartService
    {
        Task<bool?> AddShoppingCartAsync(ShoppingCart shoppingCart);
        Task<bool?> UpdateShoppingCartAsync(int ShoppingCartId, ShoppingCart shoppingCart);
        Task<bool?> DeleteShoppingCartAsync(int shoppingCartId);
        Task<ShoppingCart> GetShoppingCartByIdAsync(int shoppingCartId);
        Task<IEnumerable<ShoppingCart>> GetAllShoppingCartsAsync();
        Task<ShoppingCart> GetShoppingCartByUserIdAsync(string userId);
    }
}
