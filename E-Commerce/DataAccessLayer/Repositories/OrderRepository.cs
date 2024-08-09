using EcomGalaxy.DataAccess.Repositories.IRepository;
using EcomGalaxy.Domain.Models.Context;
using EcomGalaxy.Domain.Models.Order;

namespace EcomGalaxy.DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MyContext _context;

        public OrderRepository(MyContext context)
        {
            _context = context;
        }

        public async Task<bool?> AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool?> DeleteOrderAsync(int orderId)
        {
            var existingOrder = await GetOrderByIdAsync(orderId);
            if (existingOrder == null)
            {
                throw new InvalidOperationException($"Order with ID {orderId} not found.");
            }
            _context.Orders.Remove(existingOrder);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Where(o => o.CustomerId == userId)
                .ToListAsync();
        }

        public async Task<bool?> UpdateOrderAsync(int orderId, Order order)
        {
            var existingOrder = await GetOrderByIdAsync(orderId);
            if (existingOrder == null)
            {
                throw new InvalidOperationException($"Order with ID {orderId} not found.");
            }
            _context.Entry(existingOrder).CurrentValues.SetValues(order);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
