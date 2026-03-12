using EcomGalaxy.ApplicationLayer.Services.IServices;
using EcomGalaxy.DataAccess.Repositories.IRepository;
using EcomGalaxy.Domain.Models.Order;

namespace EcomGalaxy.ApplicationLayer.Services
{
    public class OrderItemsService : IOrderItemsService
    {
        private readonly IOrderItemsRepository _orderItemsRepository;

        public OrderItemsService(IOrderItemsRepository orderItemsRepository)
        {
            _orderItemsRepository = orderItemsRepository;
        }

        public async Task AddOrderItemAsync(OrderItem orderItem)
            => await _orderItemsRepository.AddOrderItemAsync(orderItem);

        public async Task UpdateOrderItemAsync(int orderItemId, OrderItem orderItem)
            => await _orderItemsRepository.UpdateOrderItemAsync(orderItemId, orderItem);

        public async Task DeleteOrderItemAsync(int orderItemId)
            => await _orderItemsRepository.DeleteOrderItemAsync(orderItemId);

        public async Task<OrderItem?> GetOrderItemByIdAsync(int orderItemId)
            => await _orderItemsRepository.GetOrderItemByIdAsync(orderItemId);

        public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
            => await _orderItemsRepository.GetAllOrderItemsAsync();

        public async Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
            => await _orderItemsRepository.GetOrderItemsByOrderIdAsync(orderId);

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByUserIdAsync(string sellerId)
            => await _orderItemsRepository.GetOrderItemsByUserIdAsync(sellerId);
    }
}