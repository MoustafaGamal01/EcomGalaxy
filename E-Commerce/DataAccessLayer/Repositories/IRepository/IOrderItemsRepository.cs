using EcomGalaxy.Domain.Models.Order;

namespace EcomGalaxy.DataAccess.Repositories.IRepository
{
    public interface IOrderItemsRepository
    {
        Task AddOrderItemAsync(OrderItem orderItem);
        Task UpdateOrderItemAsync(int orderItemId, OrderItem orderItem);
        Task DeleteOrderItemAsync(int orderItemId);

        // Read — single
        Task<OrderItem?> GetOrderItemByIdAsync(int orderItemId);

        Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync();
        Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
        Task<IEnumerable<OrderItem>> GetOrderItemsByUserIdAsync(string sellerId);

        Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdsAsync(IEnumerable<int> orderIds);
    }
}