using EcomGalaxy.Models.Context;

namespace EcomGalaxy.Services
{
    public class OrderItemsService : IOrderItemsService
    {
        //private readonly MyContext _context;
        private readonly IOrderItemsRepository _orderItemsRepository;

        public OrderItemsService(MyContext context, IOrderItemsRepository orderItemsRepository)
        {
            //_context = context;
            _orderItemsRepository = orderItemsRepository;
        }

        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
            await _orderItemsRepository.AddOrderItemAsync(orderItem); 
        }

        public async Task DeleteOrderItemAsync(int orderItemId)
        {
            await _orderItemsRepository.DeleteOrderItemAsync(orderItemId);
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
        {
            return await _orderItemsRepository.GetAllOrderItemsAsync();
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(int orderItemId)
        {
            return await _orderItemsRepository.GetOrderItemByIdAsync(orderItemId);
        }

        public async Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            return await _orderItemsRepository.GetOrderItemsByOrderIdAsync(orderId);
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByUserIdAsync(string sellerId)
        {
            return await _orderItemsRepository.GetOrderItemsByUserIdAsync(sellerId);
        }

        public async Task UpdateOrderItemAsync(int orderItemId, OrderItem orderItem)
        {
            await _orderItemsRepository.UpdateOrderItemAsync(orderItemId, orderItem);
        }
    }
}
