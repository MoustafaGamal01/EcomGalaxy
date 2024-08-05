namespace EcomGalaxy.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _cartRepository;

        public ShoppingCartService(IShoppingCartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<ShoppingCart> GetOrCreateShoppingCartAsync(string userId)
        {
            var cart = await _cartRepository.GetShoppingCartByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new ShoppingCart { ApplicationUserId = userId };
                await _cartRepository.AddShoppingCartAsync(cart);
                await _cartRepository.SaveChangesAsync();
            }
            return cart;
        }

        public async Task AddToCartAsync(string userId, int productId)
        {
            var cart = await GetOrCreateShoppingCartAsync(userId);
            var cartItem = await _cartRepository.GetShoppingCartItemAsync(cart.Id, productId);

            if (cartItem == null)
            {
                cartItem = new ShoppingCartItem
                {
                    ShoppingCartId = cart.Id,
                    ProductId = productId,
                    Quantity = 1
                };
                await _cartRepository.AddShoppingCartItemAsync(cartItem);
            }
            else
            {
                cartItem.Quantity += 1;
            }

            await _cartRepository.SaveChangesAsync();
        }

        public async Task RemoveFromCartAsync(int productId, int cartId)
        {
            var cartItem = await _cartRepository.GetShoppingCartItemAsync(cartId, productId); 
            if (cartItem != null)
            {
                await _cartRepository.RemoveShoppingCartItemAsync(cartItem);
                await _cartRepository.SaveChangesAsync();
            }
        }

        public async Task UpdateCartItemQuantityAsync(int producId, int cartId, int quantity)
        {
            var cartItem = await _cartRepository.GetShoppingCartItemAsync(cartId, producId); 
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                await _cartRepository.SaveChangesAsync();
            }
        }
    }
}
