namespace EcomGalaxy.Repositories.IRepository
{
    public interface IShoppingCartRepository
    {
        Task<bool?> AddShoppingCart(ShoppingCart shoppingCart);
        Task<bool?> UpdateShoppingCart(int ShoppingCartId, ShoppingCart shoppingCart);
        Task<bool?> DeleteShoppingCart(int shoppingCartId);
        Task<ShoppingCart> GetShoppingCartById(int shoppingCartId);
        Task<IEnumerable<ShoppingCart>> GetAllShoppingCarts();
        Task<ShoppingCart> GetShoppingCartByUserId(string userId);
    }
}
