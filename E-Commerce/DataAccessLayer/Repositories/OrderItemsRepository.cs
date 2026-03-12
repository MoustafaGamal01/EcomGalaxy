using EcomGalaxy.DataAccess.Repositories.IRepository;
using EcomGalaxy.Domain.Models.Context;
using EcomGalaxy.Domain.Models.Order;
using Microsoft.EntityFrameworkCore;

namespace EcomGalaxy.DataAccess.Repositories
{
    public class OrderItemsRepository : IOrderItemsRepository
    {
        private readonly MyContext _context;

        public OrderItemsRepository(MyContext context)
        {
            _context = context;
        }

        // ── Write ────────────────────────────────────────────────────────────────

        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
            if (orderItem == null) throw new ArgumentNullException(nameof(orderItem));

            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderItemAsync(int orderItemId, OrderItem orderItem)
        {
            if (orderItem == null) throw new ArgumentNullException(nameof(orderItem));

            // FindAsync hits the change tracker first, then DB — correct for mutations
            var existing = await _context.OrderItems.FindAsync(orderItemId)
                ?? throw new KeyNotFoundException($"OrderItem {orderItemId} not found.");

            _context.Entry(existing).CurrentValues.SetValues(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderItemAsync(int orderItemId)
        {
            var existing = await _context.OrderItems.FindAsync(orderItemId)
                ?? throw new KeyNotFoundException($"OrderItem {orderItemId} not found.");

            _context.OrderItems.Remove(existing);
            await _context.SaveChangesAsync();
        }

        // ── Read ─────────────────────────────────────────────────────────────────

        public async Task<OrderItem?> GetOrderItemByIdAsync(int orderItemId)
        {
            return await _context.OrderItems
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == orderItemId);
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
        {
            return await _context.OrderItems
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            // No .Include(o => o.Order) — FK filter doesn't need the nav property loaded
            return await _context.OrderItems
                .AsNoTracking()
                .Where(o => o.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByUserIdAsync(string sellerId)
        {
            if (string.IsNullOrEmpty(sellerId)) throw new ArgumentNullException(nameof(sellerId));

            // No .Include(o => o.Seller) — not needed for FK filter
            return await _context.OrderItems
                .AsNoTracking()
                .Where(o => o.SellerId == sellerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdsAsync(IEnumerable<int> orderIds)
        {
            if (orderIds == null || !orderIds.Any())
                return Enumerable.Empty<OrderItem>();

            return await _context.OrderItems
                .AsNoTracking()
                .Where(o => orderIds.Contains(o.OrderId))
                .ToListAsync();
        }
    }
}