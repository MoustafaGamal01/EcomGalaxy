namespace EcomGalaxy.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
        {
            this._shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<bool?> AddShoppingCartAsync(ShoppingCart shoppingCart)
        {
            return await _shoppingCartRepository.AddShoppingCart(shoppingCart);
        }

        public async Task<bool?> DeleteShoppingCartAsync(int shoppingCartId)
        {
            return await _shoppingCartRepository.DeleteShoppingCart(shoppingCartId);
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllShoppingCartsAsync()
        {
            return await _shoppingCartRepository.GetAllShoppingCarts();
        }

        public async Task<ShoppingCart> GetShoppingCartByIdAsync(int shoppingCartId)
        {
            return await _shoppingCartRepository.GetShoppingCartById(shoppingCartId);
        }

        public async Task<ShoppingCart> GetShoppingCartByUserIdAsync(string userId)
        {
            return await _shoppingCartRepository.GetShoppingCartByUserId(userId);
        }

        public async Task<bool?> UpdateShoppingCartAsync(int ShopCartId, ShoppingCart shoppingCart)
        {
            return await _shoppingCartRepository.UpdateShoppingCart(ShopCartId, shoppingCart);
        }
    }
}
