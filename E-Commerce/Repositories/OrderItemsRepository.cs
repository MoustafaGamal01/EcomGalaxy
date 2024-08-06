using EcomGalaxy.Models;

namespace EcomGalaxy.Repositories
{
    public class OrderItemsRepository : IOrderItemsRepository
    {
        private readonly MyContext _context;

        public OrderItemsRepository(MyContext context)
        {
            _context = context;
        }

        public async Task<bool?> AddOrderItemAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool?> DeleteOrderItemAsync(int orderItemId)
        {
            var orderItem = await GetOrderItemByIdAsync(orderItemId);
            if (orderItem == null)
            {
                throw new InvalidOperationException($"Order with ID {orderItemId} not found.");
            }
            _context.OrderItems.Remove(orderItem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
        {
            return await _context.OrderItems.ToListAsync();
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(int orderItemId)
        {
            return await _context.OrderItems.FirstOrDefaultAsync(o => o.Id == orderItemId);
        }

        public async Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            return await _context.OrderItems.Include(o => o.Order).Where(o => o.OrderId == orderId).ToListAsync();
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByUserIdAsync(string sellerId)
        {
            return await _context.OrderItems.Include(o=>o.Seller).Where(o => o.SellerId == sellerId).ToListAsync();   
        }

        public async Task<bool?> UpdateOrderItemAsync(int orderItemId, OrderItem orderItem)
        {
            var existingOrderItem = await GetOrderItemByIdAsync(orderItemId);

            if (existingOrderItem == null)
            {
                throw new InvalidOperationException($"Order with ID {orderItemId} not found.");
            }
            _context.Entry(existingOrderItem).CurrentValues.SetValues(orderItem);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
