
using EcomGalaxy.Models;

namespace EcomGalaxy.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly MyContext _context;

        public ShoppingCartRepository(MyContext context)
        {
            this._context = context;
        }

        public async Task<bool?> AddShoppingCart(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Add(shoppingCart);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool?> DeleteShoppingCart(int shoppingCartId)
        {
            var existingShopCart = await GetShoppingCartById(shoppingCartId);
            if (existingShopCart == null)
            {
                throw new InvalidOperationException($"ShoppingCart with ID {shoppingCartId} not found.");
            }
            _context.ShoppingCarts.Remove(existingShopCart);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllShoppingCarts()
        {
            return await _context.ShoppingCarts.ToListAsync();
        }

        public async Task<ShoppingCart> GetShoppingCartById(int shoppingCartId)
        {
            return await _context.ShoppingCarts.FirstOrDefaultAsync(s=>s.Id == shoppingCartId);  
        }

        public Task<ShoppingCart> GetShoppingCartByUserId(string userId)
        {
            return _context.ShoppingCarts
                .Include(s => s.ApplicationUser)
                .FirstOrDefaultAsync(s => s.ApplicationUserId == userId);
        }

        public async Task<bool?> UpdateShoppingCart(int ShopCartId, ShoppingCart shoppingCart)
        {
            var existingShopCart = await GetShoppingCartById(ShopCartId);
            if (existingShopCart == null)
            {
                throw new InvalidOperationException($"ShoppingCart with ID {ShopCartId} not found.");
            }
            _context.Entry(existingShopCart).CurrentValues.SetValues(shoppingCart);
            return await _context.SaveChangesAsync() > 0;   
        }
    }
}
