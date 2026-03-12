using EcomGalaxy.DataAccess.Repositories.IRepository;
using EcomGalaxy.Domain.Models.Context;
using EcomGalaxy.Domain.Models.Order;
using Microsoft.EntityFrameworkCore;

namespace EcomGalaxy.DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MyContext _context;

        public OrderRepository(MyContext context)
        {
            _context = context;
        }


        public async Task AddOrderAsync(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(int orderId, Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

            var existing = await _context.Orders.FindAsync(orderId)
                ?? throw new KeyNotFoundException($"Order {orderId} not found.");

            _context.Entry(existing).CurrentValues.SetValues(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var existing = await _context.Orders.FindAsync(orderId)
                ?? throw new KeyNotFoundException($"Order {orderId} not found.");

            _context.Orders.Remove(existing);
            await _context.SaveChangesAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));

            // No .Include(o => o.Customer) — service layer handles user data separately
            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.CustomerId == userId)
                .OrderByDescending(o => o.OrderedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByIdsAsync(IEnumerable<int> orderIds)
        {
            if (orderIds == null || !orderIds.Any())
                return Enumerable.Empty<Order>();

            return await _context.Orders
                .AsNoTracking()
                .Where(o => orderIds.Contains(o.Id))
                .ToListAsync();
        }
    }
}