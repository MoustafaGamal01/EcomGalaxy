using EcomGalaxy.Domain.Models.Order;

namespace EcomGalaxy.ApplicationLayer.Services.IServices
{
    public interface IOrderItemsService
    {
        Task AddOrderItemAsync(OrderItem orderItem);
        Task UpdateOrderItemAsync(int orderItemId, OrderItem orderItem);
        Task DeleteOrderItemAsync(int orderItemId);

        Task<OrderItem?> GetOrderItemByIdAsync(int orderItemId);
        Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync();
        Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
        Task<IEnumerable<OrderItem>> GetOrderItemsByUserIdAsync(string sellerId);
    }
}